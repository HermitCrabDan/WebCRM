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
                                <div>
                                    Created: {{ selectedAccountMembership.creationDateString }}
                                </div>
                                <div>
                                    Created By: {{ selectedAccountMembership.createdBy }}
                                </div>
                                <div>
                                    Last Updated: {{ selectedAccountMembership.lastUpdatedDateString }}
                                </div>
                                <div>
                                    Updated By: {{ selectedAccountMembership.lastUpdatedBy }}
                                </div>
                                <div>
                                    Deleted: {{ selectedAccountMembership.deletionDateString }}
                                </div>
                                <div>
                                    Deleted By: {{ selectedAccountMembership.deletionBy }}
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
                    <br />
                    <div style="text-align:left">
                        <w-checkbox
                            v-model="showDeletedMemberships"
                            @input="updateViewableMemberships"
                            >
                            Show Deleted Memberships
                        </w-checkbox>
                    </div>
                    <br />
                    <div>
                        <w-table
                            :headers="accountMemberHeaders"
                            :items="viewableMembershipList"
                            :selectable-rows="1"
                            :loading="isLoading"
                            @row-select="selectAccountMembership($event.item)">
                        </w-table>
                    </div>
                </w-card>
            </div>
        </w-flex>
    </div>
</template>

<script>
    import axios from 'axios';
    import NewAccountMembership from '../components/NewAccountMembership.vue';

    export default {
        components: { 
            'new-account-membership':NewAccountMembership 
        },
        name:"AccountMemberList",
        data(){
            return{
                crmAccountMemberList:[],
                viewableMembershipList:[],
                accountMemberHeaders:[
                    { label:'Account ID', key:'accountID', align:'left' },
                    { label:'Member ID', key:'memberID', align:'left' },
                    { label:'Member Name', key:'memberName', align:'left' },
                    { label:'Created by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                memberIdList:[],
                
                editMembershipMode: false,
                selectedAccountMembership: {},
                showDeletedMemberships: false,

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
                handler(newVal,oldVal){
                    console.log(newVal);
                    console.log(oldVal);
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
                        this.updateViewableMemberships();
                    })
                    .catch(error => { 
                        console.log(error);
                        this.isError = true;
                    })
                    .then(()=>{this.isLoading = false;});
            },
            updateViewableMemberships(){
                this.viewableMembershipList = [];
                this.crmAccountMemberList.forEach(element => {
                    if(this.showDeletedMemberships || !element.deletionDate){
                        this.viewableMembershipList.push(element);
                    }
                });
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