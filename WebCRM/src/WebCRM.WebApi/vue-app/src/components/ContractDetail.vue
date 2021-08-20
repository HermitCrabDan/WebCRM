<template>
    <div>
            <w-notification v-model="isLoading" success bg-color="white">
                ....loading
            </w-notification>
            <w-notification v-model="isError" error bg-color="white">
                An Error Occured
            </w-notification>
        <h2>{{ contractData.id }} - {{ contractData.contractName }}</h2>
        <br />
        <w-flex justify-center>
        <w-tabs :items="tabs" card>
            <template #item-title.1>
                Contract Details
            </template>
            <template #item-content.1>
                <w-flex justify-center fill-width>
                    <w-card title="Selected Contract" title-class="blue-light5--bg pa3" style="min-width:400px">
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
                            v-model="editContractValid"
                            @success="onValidationSuccess"
                            >
                            <div class="message-box">
                                <w-alert
                                    class="my0 text-light"
                                    v-if="editContractValid === false"
                                    error
                                    no-border>
                                    The form has errors.
                                </w-alert>
                            </div>
                            <w-button
                                @click="closeClick" 
                                sm 
                                outline 
                                round 
                                absolute 
                                icon="wi-cross" >
                            </w-button>
                            <br />
                            <div>
                                <w-input
                                    label="Contract Name"
                                    v-model="contractData.contractName"
                                    :validators="[validators.required]">
                                </w-input>
                            </div>
                            <br />
                            <div>
                                <w-input
                                    label="Contract Amount"
                                    v-model="contractData.contractAmount"
                                    :validators="[validators.required]">
                                </w-input>
                            </div>
                            <br />
                            <div>
                                <w-flex>
                                <w-input 
                                    label="Start Date" 
                                    v-model="contractData.contractStartDateString"
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
                                    v-model="contractData.contractEndDateString"
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
                            <br />
                            <div>
                                <w-button
                                    class="my1"
                                    type="submit">
                                    Edit Contract
                                </w-button>
                            </div>
                        </w-form>
                    </w-card>
                </w-flex>
            </template>
            <template #item-title.2>
                Transactions
            </template>
            <template #item-content.2>
                <div v-if="newTransactionMode">
                    <new-crm-transaction
                        :SelectedContractID="contractData.id"
                        @transactionClose="closeClick"
                        @transactionValidated="submitNewTransaction"
                        >
                    </new-crm-transaction>
                </div>
                <div v-else>
                    <br />
                    <div>
                        <w-button @click="newTransactionMode = true">New Transaction</w-button>
                    </div>
                    <br />
                    <contract-transaction-list
                        :SelectedContractID="contractData.id"
                        @contractSelected="selectTransaction"
                        >
                    </contract-transaction-list>
                </div>
            </template>
        </w-tabs>
        </w-flex>
    </div>
</template>

<script>
    import VueCal from 'vue-cal';
    import 'vue-cal/dist/vuecal.css';
    import NewCRMTransaction from '../components/NewCRMTransaction.vue';
    import ContractTransactionList from '../components/ContractTransactionList.vue';
    import axios from 'axios';
    
    export default {
        name:"ContractDetail",
        components:{
            'vue-cal':VueCal,
            'new-crm-transaction':NewCRMTransaction,
            'contract-transaction-list':ContractTransactionList,
        },
        props:{
            selectedContractData: Object
        },
        emits:["contractDetailClose","contractEditValid"],
        data(){
            return{
                contractData:{},
                tabs: [
                    { title: '', content: 'Contract Details' },
                    { title: 'Transactions', content: 'Transactions' }
                ],
                editContractValid:null,
                showEndDate:false,
                showStartDate:false,
                newTransactionMode:false,
                editTransactionMode:false,
                isLoading:false,
                isError:false,
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedContractData:{
                immediate:true,
                deep:true,
                handler(newVal, oldVal){
                    this.contractData = newVal;
                    console.log(oldVal);
                }
            }
        },
        methods:{
            selectTransaction(transactionData){
                console.log(transactionData);
            },
            submitNewTransaction(transactionData){
                console.log(transactionData);
                this.isLoading = true;
                axios
                    .post('api/ContractTransactionData', transactionData)
                    .then(response => {
                        console.log(response.data);
                    })
                    .catch(error => {
                        console.log(error);
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if(!this.isError){
                            this.newTransactionMode = false;
                        }
                    });
            },
            setStartDate(event){
                console.log(event);
                this.contractData.contractStartDateString = event.format('MM-DD-YYYY'); 
                this.contractData.contractStartDate = event;
                this.showStartDate = false;
            },
            setEndDate(event){
                console.log(event);
                this.contractData.contractEndDateString = event.format('MM-DD-YYYY'); 
                this.contractData.contractEndDate = event;
                this.showEndDate = false;
            },
            closeClick(){
                this.$emit("contractDetailClose");
            },
            onValidationSuccess(){
                this.$emit("contractEditValid", this.contractData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>