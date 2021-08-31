<template>
    <div>

    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name:"ContractExpenseList",
        props:{
            selectedContractId:Number,
        },
        data(){
            return{
                contractExpenseList:[],
                viewableContractExpenses:[],
                contractExpenseHeaders:[
                    { label:'', key:'', align:'left' }
                ],
                
                selectedExpense:{},
                showDeletedExpenses:false,

                editMode:false,
                newMode:false,

                isLoading: false,
                isError: false,
                apiUrl:'api/ContractExpenseData',

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
            updateViewableExpenses(){
                this.viewableContractExpenses = [];
                this.contractExpenseList.forEach(element => {
                    if(this.showDeletedExpenses || !element.deletionDate){
                        this.viewableContractExpenses.push(element);
                    }
                });
            },
            loadContractExpenses(){
                this.isLoading = true;
                this.isError = false;
                axios
                    .get(this.apiUrl + '/' + this.selectedContractId)
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
                        if (!this.isError){
                            this.updateViewableExpenses();
                        }
                    });
            }
        },
        watch:{
            selectedContractId:{
                immediate:true,
                deep:true,
                handler(newVal, oldVal){
                    console.log(oldVal);
                    console.log(newVal);
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