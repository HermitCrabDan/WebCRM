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
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name:"ApiManager",
        props:{
            apiUrl:String,
            parentId:Number,
            retrieveById:Boolean,
        },
        emits:["listDataLoaded","dataCreated","dataUpdated","dataDeleted"],
        data(){
            return{
                listData:[],

                isLoading:false,
                isError:false,

                errorData:{validationErrorMessages:[]},
            }
        },
        methods:{
            loadListData(){
                this.isLoading = true;
                this.isError = false;
                getUrl = this.apiUrl;
                if (this.retrieveById){
                    getUrl = this.apiUrl + '/' + this.parentId;
                }
                axios
                    .get(getUrl)
                    .then(response => {
                        this.listData = response.data;
                    })
                    .catch(error => {
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if (!this.isError){
                            this.$emit("listDataLoaded", this.listData);
                        }
                    });
            },
            postData(dataToPost){
                this.isLoading = true;
                this.isError = false;
                axios
                    .post(this.apiUrl, dataToPost)
                    .then(response => {
                        console.log(response.data);
                    })
                    .catch(error => {
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if (!this.isError){
                            this.$emit("dataCreated");
                        }
                    });
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>
