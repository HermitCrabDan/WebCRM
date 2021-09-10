<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Selected Membership" title-class="blue-light5--bg pa3" >
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="editFormValid === false"
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
                <model-details
                    :modelData="selectedAccountMembership"
                    >
                </model-details>
                <div style="min-width:600px; text-align:left; border:solid 1px silver; padding:5px">
                    <div>
                        Member Name: {{ selectedAccountMembership.memberName }}
                    </div>
                    <div>
                        Member ID: {{ selectedAccountMembership.memberID }}
                    </div>
                    <div>
                        Primary Account Membership: {{ selectedAccountMembership.isPrimaryAccountMemberString }}
                    </div>
                </div>
                <br />
                <w-flex justify-center fill-width>
                    <div style="min-width:600px; text-align:center; border:solid 1px silver; padding:5px">
                        <w-form
                            v-model="editFormValid"
                            @success="onValidationSuccess"
                            >
                            <h3>Edit Membership</h3>
                            <hr />
                            <br />
                            <div>
                                <w-checkbox
                                    v-model="selectedAccountMembership.isPrimaryAccountMember">
                                    Is Primary Membership
                                </w-checkbox>
                            </div>
                            <br />
                            <div>
                                <w-select
                                    label="Selected Member"
                                    v-model="newMemberID"
                                    :items="availableMemberList"
                                    :validators="[validators.required]"
                                    >
                                </w-select>
                            </div>
                            <br />
                            <div v-if="selectedAccountMembership.deletionDate">
                                <w-button @click="unDeleteMembershipClick">Reinstate Account Membership</w-button>
                            </div>
                            <div v-else>
                                <div>
                                    <w-button
                                        class="my1"
                                        type="submit"
                                        >
                                        Edit Membership
                                    </w-button>
                                </div>
                                <br />
                                <br />
                                <div>
                                    <w-button @click="removeMembershipClick">Remove Account Membership</w-button>
                                </div>
                            </div>
                        </w-form>
                    </div>
                </w-flex>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import axios from 'axios';
    import ModelDetails from '../../ModelDetails.vue';

    export default {
        name:"AccountMembershipDetail",
        components:{
            'model-details':ModelDetails,
        },
        emits:[
            "editMembershipValidationSuccess",
            "removeMembership",
            "unDeleteMembership",
            "closeMembershipDetail"
        ],
        props:{
            SelectedAccountMembershipData:Object,
            existingMembershipIds: Array,
        },
        data(){
            return{
                editFormValid:null,
                selectedAccountMembership:{
                    memberID:0,
                    isPrimaryAccountMember:false,
                },
                newMemberId:0,
                memberDataList:[],
                availableMemberList: [],
                selectedMemberForList:{},

                isError:false,
                isLoading:false,
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        methods:{
            removeMembershipClick(){
                this.$emit("removeMembership", this.selectedAccountMembership);
            },
            unDeleteMembershipClick(){
                this.$emit("unDeleteMembership",this.selectedAccountMembership);
            },
            closeClick(){
                this.$emit("closeMembershipDetail");
            },
            onValidationSuccess(){
                this.selectedAccountMembership.memberID = this.newMemberId;
                this.$emit("editMembershipValidationSuccess", this.selectedAccountMembership);
            },
            loadMemberships(){
                this.isError = false;
                this.isLoading = true;
                axios
                    .get("api/MemberData")
                    .then(response => { 
                        this.memberDataList = response.data;
                    })
                    .catch(error => { 
                        console.log(error);
                        this.isError = true;
                    })
                    .then(()=>{
                        this.isLoading = false;
                        if (!this.isError){
                            this.updateAvailabeAccounts();
                        }
                    });
            },
            updateAvailabeAccounts(){
                const filteredMemberList = [];
                this.memberDataList.forEach(element => {
                    if(this.selectedAccountMembership.memberID == element.id 
                        || !this.existingMembershipIds.includes(element.id)){
                            filteredMemberList.push({ label:element.memberName, value: element.id});
                    }
                });
                this.availableMemberList = filteredMemberList;
            },
        },
        watch:{
            SelectedAccountMembershipData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    if (newVal){
                        this.selectedAccountMembership = newVal;   
                        this.loadMemberships();
                    }
                }
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>