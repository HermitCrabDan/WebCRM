<template>
    <div>
        <h1>Contracts</h1>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name:"Contracts",
    data(){
        return{
            contractList: null,
            isError: false,
            isLoading: false
        }
    },
    methods:{
        getContractList() {
            this.isLoading = true;
            axios
                .get("api/ContractData")
                .then(response => {
                    this.contractList = response.data;
                    this.isError = false;
                    this.isLoading = false;
                })
                .catch(function (error) {
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