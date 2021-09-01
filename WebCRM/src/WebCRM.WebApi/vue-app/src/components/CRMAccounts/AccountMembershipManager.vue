<template>
    <div>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <w-flex justify-center fill-width>
            <div v-if="newMembershipMode">
                <new-account-membership
                    :selectedAccountId="selectedAccountId"
                    :existingMembershipIds="memberIdList"
                    @newMembershipSuccess="submitNewMembership"
                    @newMembershipClose="closeNewMembership"
                    >
                </new-account-membership>
            </div>
            <div v-else-if="editMembershipMode">
                <div>
                    <w-flex justify-center fill-width>
                        <w-card title="Selected Membership" title-class="blue-light5--bg pa3" >
                            <w-button
                                @click="editMembershipMode = false" 
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
                                    Membership Id: {{ selectedAccountMembership.id }}
                                </div>
                                <div>
                                    Account Id: {{ selectedAccountMembership.accountID }}
                                </div>
                                <div>
                                    Member Name: {{ selectedAccountMembership.memberName }}
                                </div>
                                <div>
                                    Member ID: {{ selectedAccountMembership.memberID }}
                                </div>
                                <div>
                                    Primary Account Membership: {{ selectedAccountMembership.isPrimaryAccountMember }}
                                </div>
                            </div>
                            <br />
                            <div v-if="selectedAccountMembership.deletionDate">
                                <w-button @click="unDeleteMembership">Reinstate Account Membership</w-button>
                            </div>
                            <div v-else>
                                <w-button @click="removeMembership">Remove Account Membership</w-button>
                            </div>
                        </w-card>
                    </w-flex>
                </div>
            </div>
            <div v-else>
                <div>
                    <h2>Account Memberships</h2>
                </div>
                <br />
                <div>
                    <w-button
                        @click="createNewMembership">
                        Create New Membership
                    </w-button>
                </div>
                <br />
                <w-card title="Existing Memberships" title-class="blue-light5--bg pa3" >
                    <model-list-base
                        :dataList="crmAccountMemberList"
                        :headerList="accountMemberHeaders"
                        :sortString="membershipSort"
                        @dataItemSelected="selectAccountMembership"
                        >
                    </model-list-base>
                </w-card>
            </div>
        </w-flex>

    </div>
</template>

<script>
    import axios from 'axios';
    import ModelDetails from '../ModelDetails.vue';
    import ModelListBase from '../ModelListBase.vue';
    import NewAccountMembership from './NewAccountMembership.vue'

    export default {
        name:"AccountMembershipManager",
        components:{
            'model-list-base':ModelListBase,
            'new-account-membership':NewAccountMembership,
            'model-details':ModelDetails,
        },
        data(){
            return{
                crmAccountMemberList:[],
                accountMemberHeaders:[
                    { label:'Account ID', key:'accountID', align:'left' },
                    { label:'Account Name', key:'accountName', align:'left' },
                    { label:'Member ID', key:'memberID', align:'left' },
                    { label:'Member Name', key:'memberName', align:'left' },
                    { label:'Created by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                membershipSort:'-createdBy',
                memberIdList:[],
                
                editMembershipMode: false,
                selectedAccountMembership: {},

                newMembershipMode: false,
                
                isLoading:false,
                isError:false,
            }
        },
        props:{
            selectedAccountId: Number,
        },
        watch:{
            selectedAccountId:{
                immediate:true,
                deep: true,
                handler(newVal){
                    console.log(newVal);
                    this.loadAccountMemberships();
                }
            }
        },
        methods:{
            loadAccountMemberships(){
                this.isLoading = true;
                this.isError = false;
                axios
                    .get("api/AccountMembershipData/" + this.selectedAccountId)
                    .then(response => { 
                        this.crmAccountMemberList = response.data;
                        this.memberIdList = this.crmAccountMemberList.map(x => x.memberID);
                        console.log(response.data);
                    })
                    .catch(error => { 
                        console.log(error);
                        this.isError = true;
                    })
                    .then(()=>{this.isLoading = false;});
            },
            selectAccountMembership(accountMembership){
                this.selectedAccountMembership = accountMembership;
                console.log(this.selectedAccountMembership);
                this.newMembershipMode = false;
                this.editMembershipMode = true;
            },
            createNewMembership(){
                this.editMembershipMode = false;
                this.newMembershipMode = true;
            },
            closeNewMembership(){
                this.newMembershipMode = false;
            },
            submitNewMembership(membershipData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .post("api/AccountMembershipData", membershipData)
                    .then(response => { 
                        console.log(response.data);
                        this.newMembershipMode = false;
                    })
                    .catch(error => { 
                        console.log(error);
                        this.isError = true;
                    })
                    .then(()=>{
                        this.isLoading = false;
                        if(!this.isError){
                            this.loadAccountMemberships();
                        }
                    });
            },
            unDeleteMembership(){
                this.isLoading = false;
                this.isError = false;
                this.selectedAccountMembership.deletionDate = null;
                this.selectedAccountMembership.deletionBy = '';
                axios
                    .put('api/AccountMembershipData', this.selectedAccountMembership)
                    .then(response => {
                        console.log(response.data);
                        this.editMembershipMode = false;
                    })
                    .catch(error => {
                        console.log(error);
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if (!this.isError){
                            this.loadAccountMemberships();
                        }
                    });
            },
            removeMembership(){
                this.isLoading = true;
                this.isError = false;
                axios
                    .delete("api/AccountMembershipData/" + this.selectedAccountMembership.id)
                    .then(response => { 
                        console.log(response.data);
                        this.editMembershipMode = false;
                    })
                    .catch(error => { 
                        console.log(error);
                        this.isError = true;
                    })
                    .then(()=>{
                        this.isLoading = false;
                        if(!this.isError){
                            this.loadAccountMemberships();
                        }
                    });
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>