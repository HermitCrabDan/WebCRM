<template>
    <div>

    </div>
</template>

<script>
    export default {
        name:"ContractTransactionManager",
        props:{
            SelectedContractId:Number,
        },
        data(){
            return{
                contractID:0,
                transactionList:[],
                transactionHeaders:[
                    { label:'Transaction ID', key:'id', align:'center' },
                    { label:'Transaction Date', key:'transactionDateString', align:'center' },
                    { label:'Transaction Amount', key:'transactionAmountString', align:'center' },
                    { label:'Entered by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                transactionSort:'-creationDate',

                newMode:false,
                editMode:false,
                selectedTransaction:{},

                isLoading:false,
                isError:false,
            }
        },
        watch:{
            SelectedContractId:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.contractID = newVal;
                }
            }
        },
        methods:{
            selectTransaction(transactionData){
                this.selectTransaction = transactionData;
                this.newMode = false;
                this.editMode = true;
            },
            loadTransactions(){
                this.transactionList = [];
                if (this.contractID > 0){
                    this.isLoading = false;
                    axios
                        .get('api/ContractTransactionData/' + this.contractID)
                        .then(response => {
                            this.transactionList = response.data;
                        })
                        .catch(error => {
                            this.isError = true;
                            console.log(error);
                        })
                        .then(() => {
                            this.isLoading = false;
                        });
                }
            }
        },
        mounted(){
            this.loadTransactions();
        }
    }
</script>

<style lang="scss" scoped>

</style>