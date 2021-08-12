<template>
    <div>
        <h1>Accounts</h1>
        <br />
        <div v-if="isLoading">
            ....loading
        </div>
        <div v-if="isError">
            An Error Occured
        </div>
        <div v-if="newAccountMode">
            <div>
                <label for="New Account Name">New Account Name</label>
                <input v-model="newAccountName" />
            </div>
            <div>
                <button @click="submitNewAccount" >Submit Account</button>
            </div>
        </div>
        <div v-else>
            <button @click="createNewAccount">Create New Account</button>
            <div>
                <table>
                    <thead>
                        <tr>
                            <th>Account Name</th>
                            <th>Added by</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in CRMAccountList" :key="item.id">
                            <td>{{ item.accountName }}</td>
                            <td>{{ item.createdBy }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name:"CRMAccounts",
    data(){
        return {
            CRMAccountList: null,
            isError: false,
            isLoading: false,
            newAccountMode: false,
            newAccountName: '',
        }
    },
    methods:{
        createNewAccount(){
            this.newAccountMode = true;
            this.newAccountName = '';
        },
        submitNewAccount(){
            this.isLoading = true;
            axios
                .post("api/CRMAccountData", { accountName: this.newAccountName })
                .then(response => { 
                    console.log(response.data);
                    this.isError = false;
                    this.isLoading = false;
                    this.newAccountMode = false;
                    this.getCRMAccountList();
                })
                .catch(error => { 
                    this.isError = true;
                    this.isLoading = false;
                    console.log(error);
                });
        },
        getCRMAccountList(){
            this.isLoading = true;
            axios
                .get("api/CRMAccountData")
                .then(response => { 
                    this.CRMAccountList = response.data;
                    this.isError = false;
                    this.isLoading = false;
                    console.log(response.data);
                })
                .catch(error => { 
                    console.log(error);
                    this.isError = true;
                    this.isLoading = false;
                });
        }
    }
    ,mounted(){
        this.getCRMAccountList();
    }
}
</script>

<style lang="scss" scoped>

</style>