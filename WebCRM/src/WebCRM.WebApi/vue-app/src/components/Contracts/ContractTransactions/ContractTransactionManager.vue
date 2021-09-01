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
            <new-contract-transaction
                :SelectedContractID="contractID"
                @transactionClose="newMode = false"
                @transactionValidated="submitTransaction"
                >
            </new-contract-transaction>
        </div>
        <div v-else-if="editMode">
            <contract-transaction-detail 
                :SelectedTransactionData="selectedTransaction"
                @editTransactionClose="editMode = false"
                @editTransactionValidated="updateTransaction"
                >
            </contract-transaction-detail>
        </div>
        <div v-else>
            <w-button @click="newMode = true">Create new Transaction</w-button>
            <div class="--bg pa12">
                <w-card title="Contract Transactions" title-class="blue-light5--bg pa3" style="min-width:400px">
                    <model-list-base
                        :dataList="transactionList"
                        :headerList="transactionHeaders"
                        :sortString="transactionSort"
                        @dataItemSelected="selectTransaction"
                        >
                    </model-list-base>
                </w-card>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import NewContractTransaction from './NewContractTransaction.vue';
    import ModelValidationErrorList from '../../ModelValidationErrorList.vue';
    import ContractTransactionDetail from './ContractTransactionDetail.vue';
    import ModelListBase from '../../ModelListBase.vue';

    export default {
        name:"ContractTransactionManager",
        props:{
            SelectedContractId:Number,
        },
        components:{
            'new-contract-transaction':NewContractTransaction,
            'model-validation-error-list':ModelValidationErrorList,
            'contract-transaction-detail':ContractTransactionDetail,
            'model-list-base':ModelListBase,
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

                errorData:{
                    ValidationErrorMessages:[],
                },

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
            submitTransaction(transactionData){
                this.isError = false;
                this.isLoading = true;
                axios
                    .post('api/ContractTransactionData', transactionData)
                    .then(response => {
                        console.log(response.data);
                    })
                    .catch(error => {
                        if (error.response.data){
                            this.errorData = error.response.data;
                        }
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if(!this.isError){
                            this.newTransactionMode = false;
                        }
                    });

            },
            updateTransaction(transactionToUpdate){
                this.isError = false;
                this.isLoading = true;
                axios
                    .put('api/ContractTransactionData', transactionToUpdate)
                    .then(response => {
                        console.log(response.data);
                    })
                    .catch(error => {
                        console.log(error);
                        this.isError = true;
                    })
                    .then(() =>{
                        this.isLoading = false;
                        if (!this.isError){
                            this.editMode = false;
                            this.loadTransactions();
                        }
                    });
            },
            selectTransaction(transactionData){
                this.selectedTransaction = transactionData;
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