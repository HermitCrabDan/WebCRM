<template>
    <div>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <w-card title="Contract Transactions" title-class="blue-light5--bg pa3" style="min-width:400px">
        <div>
            <w-input 
                v-model="keyword"
                placeholder="Search any column..."
                inner-icon-left="wi-search"
                class="mb3"
                style="text-align:left"
                >
            </w-input>
        </div>
        <br />
        <w-flex justify-center>
            <w-table
                :headers="transactionHeaders"
                :items="transactionList"
                :loading="isLoading"
                :sort="transactionSort"
                :filter="keywordFilter(keyword)"
                :selectable-rows="1"
                @row-select="selectTransaction($event.item)">
            </w-table>
        </w-flex>
        </w-card>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name:"ContractTransactionList",
        props:{
            SelectedContractID:Number,
        },
        emits:["contractSelected"],
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

                isError: false,
                isLoading: false,

                keyword:'',
                keywordFilter: keyword => item => {
                // Concatenate all the columns into a single string for a more performant lookup.
                const allTheColumns = `${item.transactionAmountString} ${item.transactionDateString} ${item.createdBy} ${item.lastUpdatedBy}`
                // Lookup the keyword variable in the string with case-insensitive flag.
                return new RegExp(keyword, 'i').test(allTheColumns)
                },
            }
        },
        watch:{
            SelectedContractID:{
                immediate:true,
                deep:true,
                handler(newVal, oldVal){
                    this.contractID = newVal;
                    console.log(oldVal);
                    this.getTransactions();
                }
            }
        },
        methods:{
            selectTransaction(data){
                this.$emit("contractSelected", data);
            },
            getTransactions(){
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
            this.getTransactions();
        }
    }
</script>

<style lang="scss" scoped>

</style>