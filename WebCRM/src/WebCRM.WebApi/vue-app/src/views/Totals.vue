<template>
    <div>
        <w-app block>
            <h1>Totals</h1>
            <br />
            <w-notification v-model="isLoading" success bg-color="white">
                ....loading
            </w-notification>
            <w-notification v-model="isError" error bg-color="white">
                An Error Occured
            </w-notification>
            <w-flex justify-center fill-width>
            <w-card title style="min-width:800px">
                <template #title>
                    <w-toolbar>
                        <div class="title3">Reports</div>
                        <div class="spacer"></div>
                        <w-menu>
                            <template #activator="{ on }">
                                <w-button
                                v-on="on"
                                outline
                                color="primary"
                                class="mr3">
                                Show menu on click
                                </w-button>
                            </template>
                        Menu content
                        </w-menu>
                    </w-toolbar>
                </template>
                <p>
                    <i><u>Under Construction</u></i>
                </p>
                <div style="text-align:left; border:solid 1px silver; padding:10px; width:400px">
                    <h1 style="text-align:center">Summary</h1>
                    <hr />
                    <w-flex>
                        Total Active Accounts: 
                        <div class="spacer"></div>
                        {{ summaryData.totalActiveAccounts }}
                    </w-flex>
                    <w-flex>
                        Total Deleted Accounts: 
                        <div class="spacer"></div>
                        {{ summaryData.totalDeletedAccounts }}
                    </w-flex>
                    <w-flex>
                        Total Active Members: 
                        <div class="spacer"></div>
                        {{ summaryData.totalActiveMembers }}
                    </w-flex>
                    <w-flex>
                        Total Deleted Members: 
                        <div class="spacer"></div>
                        {{ summaryData.totalDeletedMembers }}
                    </w-flex>
                    <w-flex>
                        Total Active Contracts: 
                        <div class="spacer"></div>
                        {{ summaryData.totalActiveContracts }}
                    </w-flex>
                    <w-flex>
                        Total Closed Contracts: 
                        <div class="spacer"></div>
                        {{ summaryData.totalClosedContracts }}
                    </w-flex>
                    <w-flex>
                        Total Deleted Contracts: 
                        <div class="spacer"></div>
                        {{ summaryData.totalDeletedContracts }}
                    </w-flex>
                </div>
                <div style="border:solid 1px silver; text-align:left; padding:10px; width:400px">
                    <h1 style="text-align:center">Active Contracts</h1>
                    <hr />
                    <w-flex>
                        Delinquent Contracts: 
                        <div class="spacer"></div>
                        {{ summaryData.activeDelinquentContracts }}
                    </w-flex>
                    <w-flex>
                        Current Contracts: 
                        <div class="spacer"></div>
                        {{ summaryData.activeCurrentContracts }}
                    </w-flex>
                    <w-flex>
                        Total Contract Amount: 
                        <div class="spacer"></div>
                        {{ summaryData.activeContractAmount }}
                    </w-flex>
                    <w-flex>
                        Total Collected: 
                        <div class="spacer"></div>
                        {{ summaryData.activeContractValueCollected }}
                    </w-flex>
                    <w-flex>
                        To Be Collected: 
                        <div class="spacer"></div>
                        {{ summaryData.activeContractValueToBeCollected }}
                    </w-flex>
                    <w-flex>
                        Expenses Paid: 
                        <div class="spacer"></div>
                        {{ summaryData.activeContractExpensesPaid }}
                    </w-flex>
                </div>
            </w-card>
            </w-flex>
        </w-app>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name:"Totals",
    data(){
        return{
            summaryData:{},
            isLoading:false,
            isError:false
        }
    },
    methods:{
        getSummaryData(){
            this.isLoading = false;
            this.isError = false;
            axios.get('api/ReportData')
            .then(reponse => {
                this.summaryData = reponse.data;
                console.log(reponse.data);
            })
            .catch(error => {
                console.log(error);
                this.isError = true;
            })
            .then(() => {
                this.isLoading = false;
            });
        }
    },
    mounted(){
        this.getSummaryData();
    }
}
</script>

<style lang="scss" scoped>

</style>