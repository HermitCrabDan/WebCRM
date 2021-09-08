<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Edit Transaction" title-class="blue-light5--bg pa3" style="min-width:600px">
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
                <model-details
                    :modelData="transactionData">
                </model-details>
                <w-form
                    v-model="transactionValid"
                    @success="onTransactionValidated">
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
                        <w-checkbox
                            label="Fee"
                            v-model="transactionData.isFee"
                            >
                        </w-checkbox>
                    </div>
                    <br />
                    <div>
                        <w-input
                            label="Payment Year"
                            v-model="transactionData.paymentYear"
                            >
                        </w-input>
                    </div>
                    <br />
                    <div>
                         <w-select
                            label="Payment Month" 
                            v-model="transactionData.paymentMonth"
                            :items="availablePaymentMonths"
                            >
                         </w-select>
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
                    <div v-if="transactionData.deletionDate">
                        <w-button @click="unDeleteClick">Reinstate Transaction</w-button>
                    </div>
                    <div v-else>
                        <div>
                            <w-button class="my1"
                                type="submit">
                                Edit Transaction
                            </w-button>
                        </div>
                        <br />
                        <div>
                            <w-button @click="removeClick">Remove Transaction</w-button>
                        </div>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import VueCal from 'vue-cal';
    import 'vue-cal/dist/vuecal.css';
    import ModelDetails from '../../ModelDetails.vue';

    export default {
        name:"ContractTransactionDetail",
        props:{
            SelectedTransactionData:Object
        },
        emits:["editTransactionClose", "editTransactionValidated","removeTransactionClick","reinstateTransactionClick"],
        components:{
            'vue-cal':VueCal,
            'model-details':ModelDetails,
        },
        data(){
            return{
                transactionData:{},
                transactionValid:null,
                showTransactionDate:false,
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
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            SelectedTransactionData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.transactionData = newVal;
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
                this.$emit("editTransactionClose");
            },
            onTransactionValidated(){
                this.$emit("editTransactionValidated", this.transactionData);
            },
            removeClick(){
                this.$emit("removeTransactionClick", this.transactionData);
            },
            unDeleteClick(){
                this.$emit("reinstateTransactionClick", this.transactionData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>