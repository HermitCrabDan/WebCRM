<template>
    <div>
         <w-flex justify-center fill-width>
            <w-card title="New Expense" title-class="blue-light5--bg pa3" style="min-width:600px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="expenseValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-drawer
                    v-model="showExpenseDate"
                    :right="true"
                    >
                    <w-button 
                        @click="showExpenseDate = false" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <br />
                    <div style="padding:50px">
                        <w-flex justify-center>
                            <vue-cal xsmall
                                ref="expenseDateCal"
                                click-to-navigate
                                :time="false"
                                active-view="month"
                                hide-view-selector
                                :disable-views="['week', 'day']"
                                :selected-date="expenseDate.expenseDateString"
                                class="vuecal--blue-theme vuecal--rounded-theme"
                                @cell-click="setExpenseDate($refs.expenseDateCal.isMonthView, $event)"
                                style="max-width: 270px;height: 290px"
                                :min-date="new Date().addDays(-3650).format()"
                                :max-date="new Date().addDays(730).format()"
                                >
                            </vue-cal>
                        </w-flex>
                    </div>
                </w-drawer>
                <w-form
                    v-model="expenseValid"
                    @success="onValidationSuccess">
                    <w-button
                        @click="closeClick" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>  
                    <br />
                    <div>
                        <w-input
                            label="Contract Id"
                            v-model="expenseData.contractId"
                            :validators="[validators.required]"
                            outline
                            readonly
                            >
                        </w-input>
                    </div>
                    <div>
                        <w-flex>
                        <w-input
                            label="Expense Date"
                            v-model="expenseData.expenseDateString"
                            :validators="[validators.required]"
                            readonly
                            >
                        </w-input>
                        <w-button
                            @click="showExpenseDate = true"
                            >
                            Set Date
                        </w-button>
                        </w-flex>
                    </div>
                    <br />
                    <div>
                        <w-input
                            label="Expense Amount"
                            v-model="expenseData.expenseAmount"
                            :validators="[validators.required]"
                            >
                        </w-input>
                    </div>
                    <br />
                    <div>
                        <w-button 
                            type="submit"
                            >
                            Submit Expense
                        </w-button>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import VueCal from 'vue-cal';
    import 'vue-cal/dist/vuecal.css';

    export default {
        name:"NewContractExpense",
        components:{
            'vue-cal':VueCal,
        },
        props:{
            selectedContractId:Number
        },
        data(){
            return{
                contractId:0,
                expenseData:{
                    contractId: 0,
                    expenseDate:null,
                    expenseDateString: '',
                    expenseAmount:0
                },
                expenseValid:null,

                showExpenseDate:false,
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        emits:["newExpenseValidated","newExpenseClose"],
        methods:{
            createNewExpense(){
                this.expenseData = { 
                    contractId: this.contractId,
                    expenseDate:null,
                    expenseDateString: '',
                    expenseAmount:0
                }
            },
            setExpenseDate(isMonthView, selectedDate){
                if (isMonthView){
                    this.expenseData.expenseDate = selectedDate;
                    this.expenseData.expenseDateString = selectedDate.format('MM-DD-YYYY'); 
                    this.showExpenseDate = false;
                }
            },
            closeClick(){
                this.$emit("newExpenseClose");
            },
            onValidationSuccess(){
                this.$emit("newExpenseValidated", this.expenseData);
            }
        },
        watch:{
            selectedContractId:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.contractId = newVal;
                    this.createNewExpense();
                }
            }
        },
        mounted(){
            this.createNewExpense();
        }
    }
</script>

<style lang="scss" scoped>

</style>