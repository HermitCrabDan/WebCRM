<template>
    <div>
        <h1>Members</h1>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name:"Members",
    data(){
        return {
            MemeberList: null,
            isError: false,
            isLoading: false
        }
    },
    methods:{
        getMemeberList(){
            this.isLoading = true;
            axios
                .get("api/MemeberData")
                .then(response => { 
                    this.MemeberList = response.data; 
                    this.isError = false;
                    this.isLoading = false;
                })
                .catch(function(error) {
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