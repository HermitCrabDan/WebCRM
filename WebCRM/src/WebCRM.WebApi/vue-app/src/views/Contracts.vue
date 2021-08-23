<template>
    <div>
        <w-app block>
            <h1>Contracts</h1>
            <br />
            <w-notification v-model="isLoading" success bg-color="white">
                ....loading
            </w-notification>
            <w-notification v-model="isError" error bg-color="white">
                An Error Occured
            </w-notification>
            <div v-if="newMode">
                <new-contract
                    @newContractClose="newMode = false"
                    @newContractValidated="submitNewContract"
                    >
                </new-contract>
            </div>
            <div v-else-if="editMode">
                <contract-detail
                    :selectedContractData="selectedContract"
                    @contractDetailClose="editMode = false"
                    @contractEditValid="updateContract">
                </contract-detail>
            </div>
            <div v-else>
                <w-button @click="createNewContract">Create New Contract</w-button>
                <div class="--bg pa12">
                    <w-card title="Existing Contracts" title-class="blue-light5--bg pa3">
                        <w-input 
                            v-model="keyword"
                            placeholder="Search any column..."
                            inner-icon-left="wi-search"
                            class="mb3"
                            style="text-align:left"
                            >
                        </w-input>
                        <w-table
                            :items="contractList"
                            :headers="contractHeaders"
                            :loading="isLoading"
                            :sort="contractSort"
                            :filter="keywordFilter(keyword)"
                            :selectable-rows="1"
                            @row-select="selectContract($event.item)"
                            >
                        </w-table>
                    </w-card>
                </div>
            </div>

        </w-app>
    </div>
</template>

<script>
import axios from 'axios';
import NewContract from '../components/NewContract.vue';
import ContractDetail from '../components/ContractDetail.vue';

export default {
    name:"Contracts",
    components:{
        'new-contract':NewContract,
        'contract-detail':ContractDetail,
    },
    data(){
        return{
            contractList: [],
            contractHeaders:[
                { label:'Contract ID', key:'id', align:'center' },
                { label:'Contract Name', key:'contractName', align:'left' },
                { label:'Account Name', key:'accountName', align:'left' },
                { label:'Member Name', key:'memberName', align:'left' },
                { label:'Start Date', key:'contractStartDateString', align:'center' },
                { label:'End Date', key:'contractEndDateString', align:'center' },
                { label:'Contract Amount', key:'contractAmountString', align:'center' },
                { label:'Monthly Estimate', key:'monthlyEstimateString', align:'center' },
                { label:'Total Amount Paid', key:'totalPaidAmountString', align:'center' },
                { label:'Amount Remaining', key:'amountRemainingString', align:'center' },
                { label:'Last Payment Recieved', key:'lastPaymentRecievedDateString', align:'center' },
                { label:'Delinquent', key:'isContractDelinquentString', align:'center'}
            ],
            contractSort:'-contractEndDate',
            isError: false,
            isLoading: false,

            newMode:false,
            editMode:false,

            selectedContract: {},

            apiUrl:'api/ContractData',
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
        createNewContract(){
            this.newMode = true;
        },
        selectContract(contractData){
            this.selectedContract = contractData;
            console.log(contractData);
            this.editMode = true;
        },
        submitNewContract(contractData){
            this.isError = false;
            this.isLoading = true;
            axios
                .post(this.apiUrl, contractData)
                .then(response => {
                    console.log(response.data);
                })
                .catch(error => {
                    console.log(error);
                    this.isError = true;
                })
                .then(() => {
                    this.isLoading = false;
                    if (!this.isError){
                        this.newMode = false;
                        this.getContractList();
                    }
                })
        },
        updateContract(contractData){
            this.isError = false;
            this.isLoading = true;
            axios
                .put(this.apiUrl, contractData)
                .then(response => {
                    console.log(response.data);
                })
                .catch(function (error) {
                    console.log(error);
                    this.isError = true;
                })
                .then(() => {
                    this.isLoading = false;
                    if (!this.isError){
                        this.editMode = false;
                        this.getContractList();
                    }
                });
        },
        getContractList() {
            this.isLoading = true;
            this.isError = false;
            axios
                .get(this.apiUrl)
                .then(response => {
                    this.contractList = response.data;
                    console.log(response.data);
                })
                .catch(function (error) {
                    console.log(error);
                    this.isError = true;
                })
                .then(() => {
                    this.isLoading = false;
                });
        }
    },
    mounted(){
        this.getContractList();
    }
}
</script>

<style lang="scss" scoped>

</style>