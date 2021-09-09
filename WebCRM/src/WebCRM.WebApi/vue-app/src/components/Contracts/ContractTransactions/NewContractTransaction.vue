<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="New Transaction" title-class="blue-light5--bg pa3" style="min-width:600px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="transactionValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-drawer
                    v-model="showTransactionDate"
                    :right="true"
                    >
                    <w-button 
                        @click="showTransactionDate = false" 
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
                                ref="transactionDateCal"
                                click-to-navigate
                                active-view="month"
                                :time="false"
                                :disable-views="['week', 'day']"
                                :selected-date="transactionData.transactionDateString"
                                class="vuecal--blue-theme vuecal--rounded-theme"
                                @cell-click="setTransactionDate($refs.transactionDateCal.isMonthView,  $event)"
                                style="max-width: 270px;height: 390px"
                                :min-date="new Date().addDays(-3650).format()"
                                :max-date="new Date().addDays(365).format()"
                                >
                            </vue-cal>
                        </w-flex>
                    </div>
                </w-drawer>
                <w-form
                    v-model="transactionValid"
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
                        <w-flex>
                        <w-input
                            label="Transaction Date"
                            v-model="transactionData.transactionDateString"
                            :validators="[validators.required]"
                            readonly
                            >
                        </w-input>
                        <w-button
                            @click="showTransactionDate = true"
                            >
                            Set Date
                        </w-button>
                        </w-flex>
                    </div>
                    <br />
                    <div>
                        <w-select
                            label="Payment Year"
                            v-model="transactionData.paymentYear"
                            :items="availablePaymentYears"
                            :validators="[validators.required]"
                            >
                        </w-select>
                    </div>
                    <br />
                    <div>
                         <w-select
                            label="Payment Month" 
                            v-model="transactionData.paymentMonth"
                            :items="availablePaymentMonths"
                            :validators="[validators.required]"
                            >
                         </w-select>
                    </div>
                    <br />
                    <div>
                        <w-input
                            label="Transaction Amount"
                            v-model="transactionData.transactionAmount"
                            >
                        </w-input>
                    </div>
                    <br />
                    <div>
                        <w-input
                            label="Fee Amount"
                            v-model="transactionData.feeAmount"
                            >
                        </w-input>
                    </div>
                    <br />
                    <div>
                        <w-button 
                            type="submit"
                            >
                            Submit Transaction
                        </w-button>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import VueCal from 'vue-cal';

    export default {
        name: "NewContractTransaction",
        components:{
            'vue-cal':VueCal
        },
        props:{
            SelectedContractID: Number,
        },
        emits:["transactionValidated","transactionClose"],
        data(){
            return{
                transactionData:{
                    contractID: 0,
                    transactionDate:null,
                    transactionDateString: '',
                    paymentMonth:'',
                    paymentYear:'',
                    isFee:false,
                    transactionAmount:''
                },
                transactionValid:null,

                showTransactionDate: false,
                availablePaymentMonths:[
                    { label:'January', value:1 },
                    { label:'February', value:2 },
                    { label:'March', value:3 },
                    { label:'April', value:4 },
                    { label:'May', value:5 },
                    { label:'June', value:6 },
                    { label:'July', value:7 },
                    { label:'August', value:8 },
                    { label:'September', value:9 },
                    { label:'October', value:10 },
                    { label:'November', value:11 },
                    { label:'December', value:12 },
                ],
                availablePaymentYears:[],

                isLoading: false,
                isError: false,
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            SelectedContractID:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.transactionData.contractID = newVal;
                }
            }
        },
        methods:{
            setTransactionDate(isMonthView, selectedDate){
                if (isMonthView){
                    this.transactionData.transactionDate = selectedDate;
                    this.transactionData.transactionDateString = selectedDate.format('MM-DD-YYYY'); 
                    this.showTransactionDate = false;
                }
            },
            closeClick(){
                this.$emit("transactionClose");
            },
            onValidationSuccess(){
                this.$emit("transactionValidated", this.transactionData);
            },
            loadPaymentYears(){
                var today = new Date();
                var maxYear = today.getFullYear() + 1;
                var paymentYears = new Array();
                for (var indexYear = maxYear; indexYear > maxYear-12; indexYear--){
                    paymentYears.push({ label:indexYear, value:indexYear });
                }
                this.availablePaymentYears = paymentYears;
            },
        },
        mounted(){
            this.loadPaymentYears();
        }
    }
</script>

<style lang="scss" scoped>

</style>