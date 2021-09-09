<template>
    <div>
        <div v-if="contractDetailMode">
            <member-contract-detail
                :selectedContractData="memberContractData"
                @memberContractDetailCloseClick="closeContractDetail"
                >
            </member-contract-detail>
        </div>
        <div v-else>
            <h3>Member Contracts</h3>
            <w-table
                :headers="contractHeaders"
                :items="contractList"
                :selectable-rows="1"
                :sort="contractSort"
                @row-select="selectContract($event.item)">
            </w-table>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import MemberContractDetail from './MemberContractDetail.vue';

    export default {
        name:"MemberContractManager",
        props:{
            selectedMemberId:Number,
        },
        components:{
            'member-contract-detail':MemberContractDetail,
        },
        data(){
            return{
                memberId:0,
                contractList:[],
                contractHeaders:[
                    { label:'Contract', key:'contractName', align:'left' },
                    { label:'Account', key:'accountName', align:'left' },
                    { label:'Start Date', key:'contractStartDateString', align:'center' },
                    { label:'End Date', key:'contractEndDateString', align:'center' },
                    { label:'Contract Amount', key:'contractAmountString', align:'center' },
                    { label:'Amount Paid', key:'totalPaidAmountString', align:'center' },
                    { label:'Remaining', key:'amountRemainingString', align:'center' },
                    { label:'Monthly Estimate', key:'monthlyEstimateString', align:'center' },
                    { label:'Last Payment Recieved', key:'lastPaymentRecievedDateString', align:'center' },
                    { label:'Delinquent', key:'isContractDelinquentString', align:'center'}
                ],
                contractSort:'-contractEndDate',
                contractDetailMode:false,
                memberContractData:{},

                isLoading:false,
                isError:false,
            }
        },
        methods:{
            selectContract(contractData){
                this.memberContractData = contractData;
                this.contractDetailMode = true;
            },
            loadContracts(){
                if (this.memberId > 0){
                    this.isLoading = true;
                    this.isError = false;
                    axios
                        .get('api/MemberContractData/' + this.memberId)
                        .then(response => {
                            this.contractList = response.data;
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
            closeContractDetail(){
                this.contractDetailMode = false;
            }
        },
        watch:{
            selectedMemberId:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.memberId = newVal;
                    this.loadContracts();
                }
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>