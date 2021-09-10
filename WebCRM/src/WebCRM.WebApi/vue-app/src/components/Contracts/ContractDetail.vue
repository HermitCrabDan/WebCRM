<template>
    <div>
        <w-notification v-model="isLoading" success bg-color="white">
            ....loading
        </w-notification>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <w-flex justify-center fill-width>
            <w-card title title-class="blue-light5--bg pa3" style="min-width:800px; max-width:1000px">
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
                <w-tabs :items="tabs" card>
                    <template #item-title.1>
                        Contract Details
                    </template>
                    <template #item-content.1>
                        <w-flex justify-center fill-width >
                            <model-details
                                :modelData="contractData">
                            </model-details>
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
                        <w-flex justify-center fill-width>
                            <div style="min-width:600px; border:solid 1px silver; padding:10px">
                                <w-drawer
                                    v-model="showStartDate"
                                    :right="true"
                                    >
                                    <w-button 
                                        @click="showStartDate = false" 
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
                                                ref="startDateCal"
                                                click-to-navigate
                                                :time="false"
                                                active-view="month"
                                                :disable-views="['week', 'day']"
                                                :selected-date="contractData.contractStartDateString"
                                                class="vuecal--blue-theme vuecal--rounded-theme"
                                                @cell-click="setStartDate($refs.startDateCal.isMonthView, $event)"
                                                style="max-width: 270px;height: 390px"
                                                :min-date="new Date().addDays(-3650).format()"
                                                :max-date="new Date().addDays(3650).format()"
                                                >
                                            </vue-cal>
                                        </w-flex>
                                    </div>
                                </w-drawer>
                                <w-drawer
                                    v-model="showEndDate"
                                    :right="true"
                                    >
                                    <w-button 
                                        @click="showEndDate = false" 
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
                                                ref="endDateCal"
                                                click-to-navigate
                                                :time="false"
                                                active-view="month"
                                                :disable-views="['week', 'day']"
                                                :selected-date="contractData.contractEndDateString"
                                                class="vuecal--blue-theme vuecal--rounded-theme"
                                                @cell-click="setEndDate($refs.endDateCal.isMonthView, $event)"
                                                style="max-width: 270px;height: 390px"
                                                :min-date="new Date().addDays(-3650).format()"
                                                :max-date="new Date().addDays(3650).format()"
                                                >
                                            </vue-cal>
                                        </w-flex>
                                    </div>
                                </w-drawer>
                                <w-form
                                    v-model="editContractValid"
                                    @success="onValidationSuccess"
                                    >
                                    <div class="message-box">
                                        <w-alert
                                            class="my0 text-light"
                                            v-if="editContractValid === false"
                                            error
                                            no-border>
                                            The form has errors.
                                        </w-alert>
                                    </div>
                                    <br />
                                    <div>
                                        <w-input
                                            label="Contract Name"
                                            v-model="contractData.contractName"
                                            :validators="[validators.required]">
                                        </w-input>
                                    </div>
                                    <br />
                                    <div>
                                        <w-input
                                            label="Contract Amount"
                                            v-model="contractData.contractAmount"
                                            :validators="[validators.required]">
                                        </w-input>
                                    </div>
                                    <br />
                                    <div>
                                        <w-input
                                            label="Payment Date"
                                            v-model="contractData.paymentDate"
                                            :validators="[validators.required]">
                                        </w-input>
                                    </div>
                                    <br />
                                    <div>
                                        <w-flex>
                                        <w-input 
                                            label="Start Date" 
                                            v-model="contractData.contractStartDateString"
                                            :validators="[validators.required]"
                                            readonly
                                            >
                                        </w-input>
                                        <w-button
                                            @click="showStartDate = true"
                                            >
                                            Set Date
                                        </w-button>
                                        </w-flex>
                                    </div>
                                    <br />
                                    <div>
                                        <w-flex>
                                        <w-input 
                                            label="End Date" 
                                            v-model="contractData.contractEndDateString"
                                            :validators="[validators.required]"
                                            readonly
                                            >
                                        </w-input>
                                        <w-button
                                            @click="showEndDate = true"
                                            >
                                            Set Date
                                        </w-button>
                                        </w-flex>
                                    </div>
                                    <br />
                                    <br />
                                    <div v-if="contractData.deletionDate">
                                        <w-button @click="unDeleteClick">Reinstate Contract</w-button>
                                    </div>
                                    <div v-else>
                                        <div>
                                            <w-button class="my1"
                                                type="submit">
                                                Edit Contract
                                            </w-button>
                                        </div>
                                        <br />
                                        <div>
                                            <w-button @click="removeClick">Remove Contract</w-button>
                                        </div>
                                    </div>
                                </w-form>
                            </div>
                        </w-flex>
                    </template>
                    <template #item-title.2>
                        Transactions
                    </template>
                    <template #item-content.2>
                        <w-flex justify-center fill-width>
                            <div style="min-width:600px">
                                <contract-transaction-manager
                                    :SelectedContractId="contractData.id">
                                </contract-transaction-manager>
                            </div>
                        </w-flex>
                    </template>
                    <template #item-title.3>
                        Expenses
                    </template>
                    <template #item-content.3>
                        <w-flex justify-center fill-width>
                            <div style="min-width:600px">
                                <contract-expense-manager
                                    :selectedContractId="contractData.id">
                                </contract-expense-manager>
                            </div>
                        </w-flex>
                    </template>
                </w-tabs>
            </w-card>
        </w-flex>

        <w-flex justify-center>
        
        </w-flex>
    </div>
</template>

<script>
    import VueCal from 'vue-cal';
    import 'vue-cal/dist/vuecal.css';
    import ModelDetails from '../ModelDetails.vue';
    import ContractExpenseManager from './ContractExpenses/ContractExpenseManager.vue';
    import ContractTransactionManager from './ContractTransactions/ContractTransactionManager.vue';
    
    export default {
        name:"ContractDetail",
        components:{
            'vue-cal':VueCal,
            'model-details':ModelDetails,
            'contract-expense-manager':ContractExpenseManager,
            'contract-transaction-manager':ContractTransactionManager,
        },
        props:{
            selectedContractData: Object
        },
        emits:["contractDetailClose","contractEditValid","reinstateContractData","removeContractData"],
        data(){
            return{
                contractData:{},
                tabs: [
                    { title: 'Contract Details', content: 'Contract Details' },
                    { title: 'Transactions', content: 'Transactions' },
                    { title: 'Expenses', content: 'Expenses' }
                ],
                editContractValid:null,
                showEndDate:false,
                showStartDate:false,
                newTransactionMode:false,
                editTransactionMode:false,
                isLoading:false,
                isError:false,
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedContractData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.contractData = newVal;
                }
            }
        },
        methods:{
            setStartDate(isMonthView, event){
                if (isMonthView){
                    this.contractData.contractStartDateString = event.format('MM-DD-YYYY'); 
                    this.contractData.contractStartDate = event;
                    this.showStartDate = false;
                }
            },
            setEndDate(isMonthView, event){
                if (isMonthView){
                    this.contractData.contractEndDateString = event.format('MM-DD-YYYY'); 
                    this.contractData.contractEndDate = event;
                    this.showEndDate = false;
                }
            },
            closeClick(){
                this.$emit("contractDetailClose");
            },
            onValidationSuccess(){
                this.$emit("contractEditValid", this.contractData);
            },
            unDeleteClick(){
                this.$emit("reinstateContractData", this.contractData);
            },
            removeClick(){
                this.$emit("removeContractData", this.contractData);
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>