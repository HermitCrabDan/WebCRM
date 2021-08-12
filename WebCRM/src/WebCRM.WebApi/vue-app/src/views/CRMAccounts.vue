<template>
    <div>
        <h1>Accounts</h1>
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
            newAccountData: null,
        }
    },
    methods:{
        newAccount(){
            this.newAccountMode = true;
        },
        getCRMAccountList(){
            this.isLoading = true;
            axios
                .get("api/CRMAccountData")
                .then(response => { 
                    this.CRMAccountList = response.data;
                    this.isError = false;
                    this.isLoading = false;
                })
                .catch(function(error){ 
                    console.log(error);
                    this.isError = true;
                    this.isLoading = false;
                });
        }
    }
}
</script>

<style lang="scss" scoped>

</style>