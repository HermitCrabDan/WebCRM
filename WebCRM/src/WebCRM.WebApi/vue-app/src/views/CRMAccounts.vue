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
                <w-flex justify-center fill-width>
                <w-card title="New Account" title-class="blue-light5--bg pa3" style="min-width:400px">
                    <div class="message-box">
                        <w-alert
                            class="my0 text-light"
                            v-if="newAccountValid === false"
                            error
                            no-border>
                            The form has {{ newAccountErrors }} errors.
                        </w-alert>
                    </div>
                    <w-form
                        v-model="newAccountValid"
                        :error-count="newAccountErrors"
                        @success="submitNewAccount"
                        >
                        <w-button 
                            @click="newAccountMode = false" 
                            sm 
                            outline 
                            round 
                            absolute 
                            icon="wi-cross" >
                        </w-button>
                        <div>
                            <label for="New Account Name">New Account Name</label>
                            <w-input 
                                label="New Account Name" 
                                v-model="newAccountName"
                                :validators="[validators.required]"
                                >
                            </w-input>
                        </div>
                        <br />
                        <div>
                            <w-button type="submit">Submit Account</w-button>
                        </div>
                    </w-form>
                </w-card>
                </w-flex>
            </div>
            <div v-else-if="accountEditMode">
                <w-flex justify-center fill-width>
                <w-card title="Edit Account" title-class="blue-light5--bg pa3" style="min-width:400px">
                    <div class="message-box">
                        <w-alert
                            class="my0 text-light"
                            v-if="editFormValid === false"
                            error
                            no-border>
                            The form has errors.
                        </w-alert>
                    </div>
                    <w-form
                        v-model="editFormValid"
                        @success="onEditSuccess"
                        >
                        <w-button
                            @click="accountEditMode = false" 
                            sm 
                            outline 
                            round 
                            absolute 
                            icon="wi-cross" >
                        </w-button>
                        <w-input
                            label="Account Name"
                            v-model="selectedCRMAccount.accountName"
                            :validators="[validators.required]"
                            >
                        </w-input>
                        <w-input 
                            label="Account Retirement Date"
                            v-model="selectedCRMAccount.accountRetirementDate"
                            >
                        </w-input>
                        <w-button
                            class="my1"
                            type="submit"
                            >
                            Edit Account
                        </w-button>
                    </w-form>
                </w-card>
                </w-flex>
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
                         @row-select="selectedCRMAccount = $event.item; accountEditMode = true">
                        </w-table>
                    </w-card>
                </div>
            </div>
        </w-app>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name:"CRMAccounts",
    data(){
        return {
            CRMAccountList: [],
            CRMAccountHeaders: [
                { label:'Account ID', key:'id', align:'left' },
                { label:'Account Name', key:'accountName', align:'left' },
                { label:'Created by', key:'createdBy', align:'left' },
                { label:'Date Entered', key:'creationDate', align:'left' },
                { label:'Last Updated', key:'lastUpdatedDate', align:'left' },
                { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                { label:'Retired', key:'accountRetirementDate', align:'left' },
            ],
            CRMAccountSort:'-creationDate',
            isError: false,
            isLoading: false,
            newAccountMode: false,
            newAccountName: '',
            newAccountErrors: null,
            newAccountValid: null,
            selectedCRMAccount:{},
            accountEditMode: false,
            editAccountErrors: null,
            editFormValid: null,
            keyword:'',
            keywordFilter: keyword => item => {
            // Concatenate all the columns into a single string for a more performant lookup.
            const allTheColumns = `${item.accountName} ${item.createdBy} ${item.lastUpdatedBy}`

            // Lookup the keyword variable in the string with case-insensitive flag.
            return new RegExp(keyword, 'i').test(allTheColumns)
            },
            validators: {
                required: value => !!value || 'This field is required',
            },
        }
    },
    methods:{
        createNewAccount(){
            this.accountEditMode = false;
            this.newAccountMode = true;
            this.newAccountName = '';
        },
        submitNewAccount(){
            this.isLoading = true;
            this.isError = false;
            axios
                .post("api/CRMAccountData", { accountName: this.newAccountName })
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
        onEditSuccess(){
            this.isLoading = true;
            this.isError = false;
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