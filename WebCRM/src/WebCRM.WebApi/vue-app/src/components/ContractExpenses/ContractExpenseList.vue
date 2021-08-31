<template>
    <div>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <w-card title="Contract Expenses" title-class="blue-light5--bg pa3">
            <div style="text-align:left">
                <w-checkbox
                    v-model="showDeletedExpenses"
                    @input="updateViewableExpenses">
                    Show Deleted Expenses
                </w-checkbox>
            </div>
            <br />
            <div>
                <w-input 
                    v-model="keyword"
                    placeholder="Search any column..."
                    inner-icon-left="wi-search"
                    class="mb3"
                    style="text-align:left"
                    >
                </w-input>
                <w-table
                    :headers="contractExpenseHeaders"
                    :items="viewableContractExpenses"
                    :selectable-rows="1"
                    :loading="isLoading"
                    :filter="keywordFilter(keyword)"
                    @row-select="selectContractExpense($event.item)">
                </w-table>
            </div>
        </w-card>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name:"ContractExpenseList",
        props:{
            selectedContractId:Number,
        },
        emits:["expenseSelected"],
        data(){
            return{
                contractID:0,
                contractExpenseList:[],
                viewableContractExpenses:[],
                contractExpenseHeaders:[
                    { label:'', key:'', align:'left' }
                ],
                
                showDeletedExpenses:false,


                isLoading: false,
                isError: false,

                keyword:'',
                keywordFilter: keyword => item => {
                // Concatenate all the columns into a single string for a more performant lookup.
                const allTheColumns = `${item.contractName} ${item.memberName} ${item.accountName} ${item.createdBy} ${item.lastUpdatedBy}`

                // Lookup the keyword variable in the string with case-insensitive flag.
                return new RegExp(keyword, 'i').test(allTheColumns)
                },

            }
        },
        methods:{
            selectContractExpense(expenseData){
                this.$emit("expenseSelected", expenseData);
            },
            updateViewableExpenses(){
                this.viewableContractExpenses = [];
                this.contractExpenseList.forEach(element => {
                    if(this.showDeletedExpenses || !element.deletionDate){
                        this.viewableContractExpenses.push(element);
                    }
                });
            },
            loadContractExpenses(){
                this.contractExpenseList = [];
                if (this.contractID > 0){
                    this.isLoading = true;
                    this.isError = false;
                    axios
                        .get('api/ContractExpenseData/' + this.contractID)
                        .then(response =>{ 
                            this.contractExpenseList = response.data; 
                        })
                        .catch(error => { 
                            console.log(error); 
                            this.isError = true;
                        })
                        .then(() => {
                            this.isLoading = false;
                            this.updateViewableExpenses();
                        });
                }
            }
        },
        watch:{
            selectedContractId:{
                immediate:true,
                deep:true,
                handler(newVal, oldVal){
                    console.log(oldVal);
                    this.contractID = newVal;
                    this.loadContractExpenses();
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