<template>
    <div>
        <w-notification v-model="isLoading" success bg-color="white">
            ....loading
        </w-notification>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <div v-if="newMode">
            <new-contract-expense
                :selectedContractId="contractId"
                @newExpenseClose="closeNewExpense"
                @newExpenseValidated="submitNewExpense"
                >
            </new-contract-expense>
        </div>
        <div v-else-if="editMode">
            <contract-expense-detail
                :SelectedExpenseData="selectedContractExpense"
                @closeExpenseDetails="closeExpenseDetails"
                @editExpenseValidated="updateExpense"
                >
            </contract-expense-detail>
        </div>
        <div v-else>
            <br />
            <w-button @click="newMode = true">Create New Expense</w-button>
            <div class="--bg pa12">
                <w-card title="Existing Expenses" title-class="blue-light5--bg pa3">
                    <model-list-base
                        :dataList="contractExpenseList"
                        :headerList="contractExpenseHeaders"
                        :sortString="contractExpenseSort"
                        @dataItemSelected="selectContractExpense"
                        >
                    </model-list-base>
                </w-card>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import ModelListBase from '../../ModelListBase.vue';
    import NewContractExpense from './NewContractExpense.vue';
    import ContractExpenseDetail from './ContractExpenseDetail.vue';

    export default {
        name:"ContractExpenseManager",
        props:{
            selectedContractId:Number,
        },
        components:{
            'model-list-base':ModelListBase,
            'new-contract-expense':NewContractExpense,
            'contract-expense-detail':ContractExpenseDetail,
        },
        data(){
            return{
                contractId:0,
                contractExpenseList:[],
                contractExpenseHeaders:[
                    { label:'ID', key:'id', align:'center' },
                    { label:'Contract Id', key:'contractID', align:'center' },
                    { label:'Expense Date', key:'expenseDateString', align:'left' },
                    { label:'Amount', key:'expenseAmountString', align:'center' },
                    { label:'Created by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                contractExpenseSort:'',

                isLoading:false,
                isError:false,

                selectedContractExpense:{},
                newMode:false,
                editMode:false,
            }
        },
        methods:{
            closeNewExpense(){
                this.newMode = false;
            },
            closeExpenseDetails(){
                this.editMode = false;
            },
            submitNewExpense(expenseData){
                this.isError = false;
                this.isLoading = true;
                axios
                    .post('api/ContractExpenseData', expenseData)
                    .then(response =>{
                        console.log(response.data);
                        this.newMode = false;
                    })
                    .catch(error =>{
                        console.log(error);
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if (!this.isError){
                            this.loadContractExpenses();
                        }
                    });
            },
            updateExpense(editedExpenseData){
                this.isError = false;
                this.isLoading = true;
                axios
                    .put('api/ContractExpenseData', editedExpenseData)
                    .then(response =>{
                        console.log(response.data);
                        this.editMode = false;
                    })
                    .catch(error =>{
                        console.log(error);
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if (!this.isError){
                            this.loadContractExpenses();
                        }
                    });
            },
            selectContractExpense(expenseData){
                this.selectedContractExpense = expenseData;
                this.newMode = false;
                this.editMode = true;
            },
            loadContractExpenses(){
                this.contractExpenseList = [];
                if (this.contractId > 0){
                    this.isLoading = true;
                    this.isError = false;
                    axios
                        .get('api/ContractExpenseData/' + this.contractId)
                        .then(response =>{ 
                            this.contractExpenseList = response.data; 
                            console.log(response.data);
                        })
                        .catch(error => { 
                            console.log(error); 
                            this.isError = true;
                        })
                        .then(() => {
                            this.isLoading = false;
                        });
                }
            }
        },
        watch:{
            selectedContractId:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.contractId = newVal;
                }
            }
        },
        mounted(){
            this.loadContractExpenses();
        }
    }
</script>

<style lang="scss" scoped>

</style>