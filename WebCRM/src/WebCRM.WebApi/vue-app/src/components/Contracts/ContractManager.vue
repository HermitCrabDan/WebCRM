<template>
    <div>
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
            <w-button @click="newMode = true">Create New Contract</w-button>
            <div class="--bg pa12">
                <w-card title="Existing Contracts" title-class="blue-light5--bg pa3">
                    <model-list-base
                        :dataList="contractList"
                        :headerList="contractHeaders"
                        :sortString="contractSort"
                        @dataItemSelected="selectContract"
                        >
                    </model-list-base>
                </w-card>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import ContractDetail from './ContractDetail.vue';
    import NewContract from './NewContract.vue';
    import ModelListBase from '../ModelListBase.vue';

    export default {
        name:"ContractManager",
        components:{
            'contract-detail':ContractDetail,
            'new-contract':NewContract,
            'model-list-base':ModelListBase,
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
            }
        },
    methods:{
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
                .catch(error => {
                    console.log(error);
                    console.log(error.response);
                    console.log(error.response.data);
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