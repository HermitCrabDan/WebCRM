<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title title-class="blue-light5--bg pa3" style="min-width:800px;">
                <template #title>
                    <div class="title3">
                        Selected Contract: {{ contractData.id }} - {{ contractData.contractName }}
                    </div>
                </template>
                <w-button
                    @click="closeClick" 
                    sm 
                    outline 
                    round 
                    absolute 
                    icon="wi-cross" >
                </w-button>
                <w-flex justify-center fill-width>
                    <div style="border:solid 1px silver; padding:5px">
                        <h3>Contract Details</h3>
                        <hr />
                        <br />
                        <div>
                            Contract Name:
                            {{ contractData.contractName }}
                        </div>
                        <div>
                            Account Name:
                            {{ contractData.accountName }}
                        </div>
                        <div>
                            Contract Amount:
                            {{ contractData.contractAmountString }}
                        </div>
                        <div>
                            Monthly Estimate:
                            {{ contractData.monthlyEstimateString }}
                        </div>
                        <div>
                            Total Amount Paid:
                            {{ contractData.totalPaidAmountString }}
                        </div>
                        <div>
                            Amount Remaining: 
                            {{ contractData.amountRemainingString }}
                        </div>
                    </div>
                    <div style="border:solid 1px silver; padding:5px">
                        <h3>Payment Schedule</h3>
                        <hr />
                        <br />
                        <div>
                            First Payment Date:
                            {{ contractData.firstPaymentDateString }}
                        </div>
                        <div>
                            Next Payment Date:
                            {{ contractData.nextPaymentDateString }}
                        </div>
                        <div>
                            Payments Reamining:
                            {{ contractData.paymentsRemaining }}
                        </div>
                        <div>
                            Final Payment Date:
                            {{ contractData.lastPaymentDateString }}
                        </div>
                        <div>
                            Last Payment Recieved:
                            {{ contractData.lastPaymentRecievedDateString }}
                        </div>
                        <div>
                            Contract Delinquent: 
                            {{ contractData.isContractDelinquentString }}
                        </div>
                    </div>
                </w-flex>
                <br />
                <div>
                    <h3>Contract Transactions</h3>
                    <w-table
                        :headers="transactionHeaders"
                        :items="contractTransactions"
                        :selectable-rows="1"
                        :sort="transactionSort"
                        @row-select="selectTransaction($event.item)">
                    </w-table>
                </div>
                <br />
                <div>
                    <h3>Contract Expenses</h3>
                    <w-table
                        :headers="contractExpenseHeaders"
                        :items="contractExpenses"
                        :selectable-rows="1"
                        :sort="contractExpenseSort"
                        @row-select="selectExpense($event.item)">
                    </w-table>
                </div>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name:"MemberContractDetail",
        props:{
            selectedContractData:Object,
        },
        emits:["memberContractDetailCloseClick"],
        data(){
            return{
                contractData:{},

                loadingTransactions:false,
                transactionLoadError:false,
                contractTransactions:[],
                transactionHeaders:[
                    { label:'Transaction Date', key:'transactionDateString', align:'center' },
                    { label:'Transaction Amount', key:'transactionAmountString', align:'center' },
                    { label:'Fee', key:'isFeeString', align:'center' },
                    { label:'Payment Year', key:'paymentYear', align:'center'},
                    { label:'Payment Month', key:'paymentMonth', align:'center'},
                    { label:'Entered by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                transactionSort:'-creationDate',
                selectedTransactionData:{},
                showSelectedExpense:false,

                loadingExpenses:false,
                expenseLoadError:false,
                contractExpenses:[],
                contractExpenseHeaders:[
                    { label:'Expense Date', key:'expenseDateString', align:'left' },
                    { label:'Amount', key:'expenseAmountString', align:'center' },
                    { label:'Created by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                contractExpenseSort:'-creationDate',
                selectedExpenseData:{},
                showSelectedExpense:false,
            }
        },
        watch:{
            selectedContractData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.contractData = newVal;
                    this.loadContractTransactions();
                    this.loadContractExpenses();
                }
            }
        },
        methods:{
            closeClick(){
                this.$emit("memberContractDetailCloseClick");
            },
            loadContractTransactions(){
                if (this.contractData.id > 0){
                    this.loadingTransactions = true;
                    this.transactionLoadError = false;
                    axios
                        .get('api/ContractTransactionData/' + this.contractData.id)
                        .then(response => {
                            this.contractTransactions = response.data;
                        })
                        .catch(error => {
                            this.transactionLoadError = true;
                            console.log(error);
                        })
                        .then(() => {
                            this.loadingTransactions = false;
                        });
                }
            },
            loadContractExpenses(){
                if (this.contractData.id > 0){
                    this.loadingExpenses = true;
                    this.expenseLoadError = false;
                    axios
                        .get('api/ContractExpenseData/' + this.contractData.id)
                        .then(response => {
                            this.contractExpenses = response.data;
                        })
                        .catch(error => {
                            this.expenseLoadError = true;
                            console.log(error);
                        })
                        .then(() => {
                            this.loadingExpenses = false;
                        });
                }
            },
            selectExpense(expenseData){
                this.selectedExpenseData = expenseData;
                this.showSelectedExpense = true;
            },
            selectTransaction(transactionData){
                this.selectedTransactionData = transactionData;
                this.showSelectedTransaction = true;
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>