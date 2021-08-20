<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="New Transaction" title-class="blue-light5--bg pa3" style="min-width:400px">
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
                                :time="false"
                                active-view="month"
                                hide-view-selector
                                :disable-views="['week', 'day']"
                                class="vuecal--blue-theme vuecal--rounded-theme"
                                @cell-focus="setTransactionDate($event)"
                                style="max-width: 270px;height: 290px">
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
                        <w-input
                            label="Transaction Amount"
                            v-model="transactionData.transactionAmount"
                            :validators="[validators.required]"
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
        name: "NewCRMTransaction",
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
                    transactionAmount:0
                },
                transactionValid:null,

                showTransactionDate: false,

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
                handler(newVal, oldVal){
                    this.transactionData.contractID = newVal;
                    console.log(oldVal);
                }
            }
        },
        methods:{
            setTransactionDate(selectedDate){
                this.transactionData.transactionDate = selectedDate;
                this.transactionData.transactionDateString = selectedDate.format('MM-DD-YYYY'); 
                this.showTransactionDate = false;
            },
            closeClick(){
                this.$emit("transactionClose");
            },
            onValidationSuccess(){
                this.$emit("transactionValidated", this.transactionData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>