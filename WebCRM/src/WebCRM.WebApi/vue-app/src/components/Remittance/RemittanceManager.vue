<template>
    <div>
        <w-notification v-model="isLoading" success bg-color="white">
            ....loading
        </w-notification>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <div class="--bg pa12">
            <w-card title="Remittance" title-class="blue-light5--bg pa3">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="transactionValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-form
                    v-model="remittanceFormValid"
                    @success="retrieveRemittanceSummary">
                    <div>
                        <w-flex justify-center fill-width>
                            <w-select style="width:200px"
                                label="Selected Year"
                                v-model="selectedYear"
                                :items="availableYears"
                                :validators="[validators.required]"
                                >
                            </w-select>
                            <w-button 
                                type="submit"
                                >
                                Load Summary
                            </w-button>
                        </w-flex>
                    </div>
                </w-form>
                <br />
                <div>
                    <w-table
                        :headers="remittanceHeaders"
                        :items="remittanceData"
                        :selectable-rows="0"
                        :sort="remittanceSort"
                        >
                    </w-table>
                </div>
            </w-card>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    
    export default {
        name:"RemittanceManager",
        data(){
            return{
                remittanceFormValid:null,
                selectedYear:null,
                availableYears:[],
                
                isError:false,
                isLoading:false,

                remittanceData:[],
                remittanceHeaders:[
                    { label:'Month End', key:'monthEndDate', align:'center' },
                    { label:'Active Contracts', key:'totalActiveContracts', align:'center' },
                    { label:'Total Value', key:'activeContractsTotalValue', align:'left' },
                    { label:'Payment Collected', key:'activeContractsPaymentCollected', align:'left' },
                    { label:'Expenses Paid', key:'activeContractsTotalExpensesPaid', align:'left' },
                    { label:'Remittance Amount', key:'activeContractsMonthlyRemmitance', align:'left' },
                    { label:'Fees Paid', key:'activeContractsFeesPaidInMonth', align:'left' },
                    { label:'Contracts Starting', key:'activeContractsFirstMonth', align:'center' },
                    { label:'Contracted Ending', key:'activeContractsClosingInMonth', align:'center' },
                    { label:'Contracts Deleted', key:'contractsDeletedInMonth', align:'center' },
                ],
                remittanceSort:'-monthEndDate',
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        methods:{
            loadAvailableYears(){
                var today = new Date();
                var maxYear = today.getFullYear();
                var paymentYears = new Array();
                for (var indexYear = maxYear; indexYear > maxYear-10; indexYear--){
                    paymentYears.push({ label:indexYear, value:indexYear });
                }
                this.availableYears = paymentYears;
            },
            retrieveRemittanceSummary(){
                if (this.selectedYear){
                    this.isError = false;
                    this.isLoading = true;
                    axios
                        .get('api/RemittanceSummaryData/' + this.selectedYear)
                        .then(response => {
                            this.remittanceData = response.data;
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
        mounted(){
            this.loadAvailableYears();
        }
    }
</script>

<style lang="scss" scoped>

</style>