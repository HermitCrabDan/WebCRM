<template>
    <div>
        <w-notification v-model="isLoading" success bg-color="white">
            ....loading
        </w-notification>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <div v-if="isError">
            <model-validation-error-list
                :errorList="errorData.validationErrorMessages">
            </model-validation-error-list>
        </div>
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
                <account-membership-detail
                    :SelectedAccountMembershipData="selectedAccountMembership"
                    @closeMembershipDetail="closeMembershipDetailClick"
                    @removeMembership="removeMembershipData"
                    @unDeleteMembership="unDeleteMembershipData"
                    >
                </account-membership-detail>
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
    import ModelListBase from '../../ModelListBase.vue';
    import ModelValidationErrorList from '../../ModelValidationErrorList.vue';
    import NewAccountMembership from './NewAccountMembership.vue'
    import AccountMembershipDetail from './AccountMembershipDetail.vue';

    export default {
        name:"AccountMembershipManager",
        components:{
            'model-list-base':ModelListBase,
            'new-account-membership':NewAccountMembership,
            'account-membership-detail':AccountMembershipDetail,
            'model-validation-error-list':ModelValidationErrorList,
        },
        data(){
            return{
                crmAccountMemberList:[],
                accountMemberHeaders:[
                    { label:'Account ID', key:'accountID', align:'left' },
                    { label:'Account Name', key:'accountName', align:'left' },
                    { label:'Member ID', key:'memberID', align:'left' },
                    { label:'Member Name', key:'memberName', align:'left' },
                    { label:'Primary Membership', key:'isPrimaryAccountMemberString', align:'center' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                membershipSort:'-lastUpdatedDate',
                memberIdList:[],
                
                editMembershipMode: false,
                selectedAccountMembership: {},

                errorData:{
                    validationErrorMessages:[]
                },

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
                    })
                    .catch(error => {
                        this.isError = true;
                        console.log(error);
                    })
                    .then(()=>{
                        this.isLoading = false;
                    });
            },
            selectAccountMembership(accountMembership){
                this.selectedAccountMembership = accountMembership;
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
            closeMembershipDetailClick(){
                this.editMembershipMode = false;
                this.loadAccountMemberships();
            },
            submitNewMembership(membershipData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .post("api/AccountMembershipData", membershipData)
                    .then(response => { 
                        this.errorData = response.data;
                        this.newMembershipMode = false;
                    })
                    .catch(error => { 
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(()=>{
                        this.isLoading = false;
                        if(!this.isError){
                            this.loadAccountMemberships();
                        }
                    });
            },
            unDeleteMembershipData(membershipData){
                this.isLoading = false;
                this.isError = false;
                membershipData.deletionDate = null;
                membershipData.deletionBy = '';
                axios
                    .put('api/AccountMembershipData', membershipData)
                    .then(response => {
                        this.errorData = response.data;
                    })
                    .catch(error => {
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if (!this.isError){
                            this.editMembershipMode = false;
                            this.loadAccountMemberships();
                        }
                    });
            },
            removeMembershipData(membershipData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .delete("api/AccountMembershipData/" + membershipData.id)
                    .then(response => { 
                        console.log(response);
                    })
                    .catch(error => { 
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(()=>{
                        this.isLoading = false;
                        if(!this.isError){
                            this.editMembershipMode = false;
                            this.loadAccountMemberships();
                        }
                    });
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>