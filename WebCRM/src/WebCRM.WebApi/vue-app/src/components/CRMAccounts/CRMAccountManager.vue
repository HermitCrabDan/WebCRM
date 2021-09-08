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
        <div v-if="newMode">
            <new-crm-account
                @newAccountClose="closeNewAccountMode"
                @newAccountValidated="submitNewAccount"
                >
            </new-crm-account>
        </div>
        <div v-else-if="editMode">
            <crm-account-detail
                :selectedAccountData="selectedCRMAccount"
                @editValidationSuccess="onEditSuccess"
                @accountDetailClose="closeAccountDetail"
                @reinstateAccountClick="reinstateAccount"
                @removeAccountClick="removeAccount"
                >
            </crm-account-detail>
        </div>
        <div v-else>
            <w-button @click="newMode = true">Create New Account</w-button>
            <div class="--bg pa12">
                <w-card title="Existing Accounts" title-class="blue-light5--bg pa3">
                    <model-list-base
                        :dataList="accountList"
                        :headerList="accountHeaders"
                        :sortString="accountSort"
                        @dataItemSelected="selectCRMAccount"
                        >
                    </model-list-base>
                </w-card>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import ModelListBase from '../ModelListBase.vue';
    import ModelValidationErrorList from '../ModelValidationErrorList.vue';
    import NewCRMAccount from './NewCRMAccount.vue';
    import CRMAccountDetail from './CRMAccountDetail.vue';

    export default {
        name:"CRMAccountManager",
        components:{
            'model-list-base':ModelListBase,
            'new-crm-account':NewCRMAccount,
            'crm-account-detail':CRMAccountDetail,
            'model-validation-error-list':ModelValidationErrorList,
        },
        data(){
            return{
                accountList:[],
                accountHeaders:[
                    { label:'Account ID', key:'id', align:'left' },
                    { label:'Account Name', key:'accountName', align:'left' },
                    { label:'Created by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                accountSort:'-creationDate',

                apiUrl:'api/CRMAccountData',

                isError:false,
                isLoading:false,

                newMode:false,
                editMode:false,

                errorData:{validationErrorMessages:[]},
                selectedCRMAccount:{}
            }
        },
        methods:{
            selectCRMAccount(accountData){
                this.selectedCRMAccount = accountData;
                this.editMode = true;
                this.newMode = false;
            },
            submitNewAccount(newAccountData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .post(this.apiUrl, newAccountData)
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
                            this.newMode = false;
                            this.loadAccountData();
                        }
                    });
            },
            closeNewAccountMode(){
                this.newMode = false;
            },
            closeAccountDetail(){
                this.editMode = false;
                this.loadAccountData();
            },
            onEditSuccess(accountData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .put(this.apiUrl, accountData)
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
                            this.editMode = false;
                            this.loadAccountData();
                        }
                    });
            },
            removeAccount(accountData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .delete(this.apiUrl + '/' + accountData.id)
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
                            this.editMode = false;
                            this.loadAccountData();
                        }
                    })
            },
            reinstateAccount(accountData){
                this.isError = false;
                this.isLoading = true;
                accountData.deletionDate = null;
                accountData.deletionBy = '';
                axios
                    .put(this.apiUrl, accountData)
                    .then(response => {
                        this.errorData = response.data;
                    })
                    .catch(error => {
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() =>{
                        this.isLoading = false;
                        if (!this.isError){
                            this.editMode = false;
                            this.loadAccountData();
                        }
                    });
            },
            loadAccountData(){
                this.isError = false;
                this.isLoading = true;
                axios
                    .get("api/CRMAccountData")
                    .then(response => { 
                        this.accountList = response.data;
                    })
                    .catch(error => { 
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(()=>{
                        this.isLoading = false;
                    });
            }
        },
        mounted(){
            this.loadAccountData();
        }
    }
</script>

<style lang="scss" scoped>

</style>