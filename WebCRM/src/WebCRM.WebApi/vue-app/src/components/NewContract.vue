<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="New Contract" title-class="blue-light5--bg pa3" style="min-width:400px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="newContractValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-drawer
                    v-model="showStartDate"
                    :right="true"
                    >
                    <w-button 
                        @click="showStartDate = false" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <br />
                    <div style="padding:50px">
                        <w-flex justify-center>
                            <vue-cal xsmall
                                :time="false"
                                active-view="month"
                                hide-view-selector
                                :disable-views="['week', 'day']"
                                class="vuecal--blue-theme vuecal--rounded-theme"
                                @cell-focus="setStartDate($event)"
                                style="max-width: 270px;height: 290px">
                            </vue-cal>
                        </w-flex>
                    </div>
                </w-drawer>
                <w-drawer
                    v-model="showEndDate"
                    :right="true"
                    >
                    <w-button 
                        @click="showEndDate = false" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <br />
                    <div style="padding:50px">
                        <w-flex justify-center>
                            <vue-cal xsmall
                                :time="false"
                                active-view="month"
                                hide-view-selector
                                :disable-views="['week', 'day']"
                                class="vuecal--blue-theme vuecal--rounded-theme"
                                @cell-focus="setEndDate($event)"
                                style="max-width: 270px;height: 290px">
                            </vue-cal>
                        </w-flex>
                    </div>
                </w-drawer>
                <w-form
                    v-model="newContractValid"
                    @success="onValidationSuccess"
                    >
                    <w-button 
                        @click="closeClick" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <div>
                        <w-input
                            label="Contract Name" 
                            v-model="newContractData.contractName"
                            :validators="[validators.required]">
                        </w-input>
                    </div>
                    <br />
                    <div>
                         <w-select
                            label="Account" 
                            v-model="newContractData.accountID"
                            :items="viewableAccounts"
                            :validators="[validators.required]"
                            >
                         </w-select>
                    </div>
                    <br />
                    <div>
                         <w-select 
                            label="Member" 
                            v-model="newContractData.memberID"
                            :items="viewableMembers"
                            :validators="[validators.required]"
                            >
                         </w-select>
                    </div>
                    <br />
                    <div>
                        <w-flex>
                         <w-input 
                            label="Start Date" 
                            v-model="newContractData.contractStartDateString"
                            :validators="[validators.required]"
                            readonly
                            >
                        </w-input>
                        <w-button
                            @click="showStartDate = true"
                            >
                            Set Date
                        </w-button>
                        </w-flex>
                    </div>
                    <br />
                    <div>
                        <w-flex>
                         <w-input 
                            label="End Date" 
                            v-model="newContractData.contractEndDateString"
                            :validators="[validators.required]"
                            readonly
                            >
                        </w-input>
                        <w-button
                            @click="showEndDate = true"
                            >
                            Set Date
                        </w-button>
                        </w-flex>
                    </div>
                    <br />
                    <div>
                         <w-input 
                            label="Contract Amount" 
                            v-model="newContractData.contractAmount"
                            :validators="[validators.required]"
                            >
                        </w-input>
                    </div>
                    <br />
                    <div>
                        <w-button type="submit">Submit Contract</w-button>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import axios from 'axios';
    import VueCal from 'vue-cal';
    import 'vue-cal/dist/vuecal.css';

    export default {
        components:{
            'vue-cal':VueCal
        },
        name:"NewContract",
        props:{
            selectedMemberID: Number,
            selectedAccountID: Number,
        },
        data(){
            return{
                newContractData:{ 
                    accountID:0, 
                    memberID: 0,
                    contractName:'',
                    contractStartDate: '',
                    contractEndDate: '',
                    contractAmount: 0.00 
                },
                newContractValid:null,
                
                showStartDate: false,
                showEndDate: false,

                availableMembers:[],
                viewableMembers: [],
                availableAccounts:[],
                viewableAccounts:[],

                isLoading: false,
                isError: false,
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedMemberID:{
                immediate:true,
                deep:true,
                handeler(newVal, oldVal){
                    this.newContractData.accountID = newVal;
                    console.log(oldVal);
                }
            },
            selectedAccountID:{
                immediate:true,
                deep:true,
                handeler(newVal, oldVal){
                    this.newContractData.accountID = newVal;
                    console.log(oldVal);
                }
            }
        },
        emits:["newContractValidated","newContractClose"],
        methods:{
            updateViewableMembers(){
                const filteredMembers = [];
                this.availableMembers.forEach(element => {
                    if(!element.deletionDate){
                        filteredMembers.push({ label:element.memberName, value: element.id})
                    }
                });
                this.viewableMembers = filteredMembers;
            },
            updateViewableAccounts(){
                const filteredAccounts = [];
                this.availableAccounts.forEach(element => {
                    if(!element.deletionDate){
                        filteredAccounts.push({ label:element.accountName, value: element.id})
                    }
                });
                this.viewableAccounts = filteredAccounts;
            },
            loadAvailableMembers(){
                this.isError = false;
                this.isLoading = true;
                axios.get('api/MemberData')
                    .then(response => {
                        this.availableMembers = response.data;
                    })
                    .catch(error => {
                        console.log(error);
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if(!this.isError){
                            this.updateViewableMembers();
                        }
                    });
            },
            loadAvailableAccounts(){
                this.isError = false;
                this.isLoading = true;
                axios.get('api/CRMAccountData')
                    .then(response => {
                        this.availableAccounts = response.data;
                    })
                    .catch(error => {
                        console.log(error);
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if(!this.isError){
                            this.updateViewableAccounts();
                        }
                    });
            },
            closeClick(){
                this.$emit("newContractClose");
            },
            onValidationSuccess(){
                this.$emit("newContractValidated", this.newContractData);
            },
            setStartDate(event){
                console.log(event);
                this.newContractData.contractStartDateString = event.format('MM-DD-YYYY'); 
                this.newContractData.contractStartDate = event;
                this.showStartDate = false;
            },
            setEndDate(event){
                console.log(event);
                this.newContractData.contractEndDateString = event.format('MM-DD-YYYY'); 
                this.newContractData.contractEndDate = event;
                this.showEndDate = false;
            }
        },
        mounted(){
            this.loadAvailableMembers();
            this.loadAvailableAccounts();
        }
    }
</script>

<style lang="scss" scoped>

</style>