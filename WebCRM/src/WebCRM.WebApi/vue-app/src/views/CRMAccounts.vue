<template>
    <div>
        <w-app block>
            <h1>Accounts</h1>
            <br />
            <w-notification v-model="isLoading" success bg-color="white">
                ....loading
            </w-notification>
            <w-notification v-model="isError" error bg-color="white">
                An Error Occured
            </w-notification>
            <div v-if="newAccountMode">
                <new-crm-account
                    @newAccountClose="closeNewAccountMode"
                    @newAccountValidated="submitNewAccount"
                    >
                </new-crm-account>
            </div>
            <div v-else-if="accountEditMode">
                <crm-account-detail
                    :selectedAccountName="selectedCRMAccountName"
                    @editValidationSuccess="onEditSuccess"
                    @accountDetailClose="closeAccountDetail"
                    @update:selectedAccountName="updateSelectedAccountName"
                    >
                </crm-account-detail>
            </div>
            <div v-else>
                <w-button @click="createNewAccount">Create New Account</w-button>
                <div class="--bg pa12">
                    <w-card title="Existing Accounts" title-class="blue-light5--bg pa3">
                        <w-input 
                            v-model="keyword"
                            placeholder="Search any column..."
                            inner-icon-left="wi-search"
                            class="mb3"
                            style="text-align:left"
                            >
                        </w-input>
                        <w-table
                        :headers="CRMAccountHeaders"
                        :items="CRMAccountList"
                        :loading="isLoading"
                        :sort="CRMAccountSort"
                        :filter="keywordFilter(keyword)"
                        :selectable-rows="1"
                         @row-select="selectCRMAccount($event.item)">
                        </w-table>
                    </w-card>
                </div>
            </div>
        </w-app>
    </div>
</template>

<script>
import axios from 'axios';
import NewCRMAccount from '../components/NewCRMAccount.vue';
import CRMAccountDetail from '../components/CRMAccountDetail.vue';

export default {
    components: { 
        'new-crm-account': NewCRMAccount,
        'crm-account-detail': CRMAccountDetail,
    },
    name:"CRMAccounts",
    data(){
        return {
            CRMAccountList: [],
            CRMAccountHeaders: [
                { label:'Account ID', key:'id', align:'left' },
                { label:'Account Name', key:'accountName', align:'left' },
                { label:'Created by', key:'createdBy', align:'left' },
                { label:'Date Entered', key:'creationDateString', align:'left' },
                { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
            ],
            CRMAccountSort:'-creationDate',

            isError: false,
            isLoading: false,
            newAccountMode: false,
            accountEditMode: false,

            selectedCRMAccount: {},
            selectedCRMAccountName: '',

            keyword:'',
            keywordFilter: keyword => item => {
            // Concatenate all the columns into a single string for a more performant lookup.
            const allTheColumns = `${item.accountName} ${item.createdBy} ${item.lastUpdatedBy}`

            // Lookup the keyword variable in the string with case-insensitive flag.
            return new RegExp(keyword, 'i').test(allTheColumns)
            },
        }
    },
    methods:{
        createNewAccount(){
            this.accountEditMode = false;
            this.newAccountMode = true;
            this.newAccountData.newAccountName = '';
        },
        selectCRMAccount(accountData){
            this.selectedCRMAccount = accountData;
            this.selectedCRMAccountName = accountData.accountName;
            this.accountEditMode = true;
            this.newAccountMode = false;
        },
        submitNewAccount(newAccountData){
            this.isLoading = true;
            this.isError = false;
            axios
                .post("api/CRMAccountData", newAccountData)
                .then(response => { 
                    console.log(response.data);
                    this.newAccountMode = false;
                    this.getCRMAccountList();
                })
                .catch(error => { 
                    this.isError = true;
                    console.log(error);
                })
                .then(() => { this.isLoading = false; });
        },
        closeNewAccountMode(){
            this.newAccountMode = false;
        },
        closeAccountDetail(){
            this.accountEditMode = false;
        },
        updateSelectedAccountName(val){
            this.selectedCRMAccountName = val;
        },
        onEditSuccess(){
            this.isLoading = true;
            this.isError = false;
            this.selectedCRMAccount.accountName = this.selectedCRMAccountName;

            axios
                .put("api/CRMAccountData", this.selectedCRMAccount)
                .then(response => { 
                    console.log(response.data);
                    this.accountEditMode = false;
                    this.getCRMAccountList();
                })
                .catch(error => { 
                    this.isError = true;
                    console.log(error);
                })
                .then(() => { this.isLoading = false; });
        },
        getCRMAccountList(){
            this.isLoading = true;
            axios
                .get("api/CRMAccountData")
                .then(response => { 
                    this.CRMAccountList = response.data;
                    this.isError = false;
                    console.log(response.data);
                })
                .catch(error => { 
                    console.log(error);
                    this.isError = true;
                })
                .then(()=>{this.isLoading = false;});
        }
    }
    ,mounted(){
        this.getCRMAccountList();
    }
}
</script>

<style lang="scss" scoped>

</style>