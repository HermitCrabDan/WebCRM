<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Edit Transaction" title-class="blue-light5--bg pa3" style="min-width:400px">
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
                <div style="padding:5px; border:solid 1px silver">
                    <div>
                        Created By: {{ transactionData.createdBy }} - {{ transactionData.creationDateString }}
                    </div>
                    <div v-if="lastUpdatedDate">
                        Last Updated: {{ transactionData.lastUpdatedBy }} - {{ transactionData.lastUpdatedDateString }}
                    </div>
                </div>
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
                            Edit Transaction
                        </w-button>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    export default {
        name:"EditCRMTransaction",
        props:{
            SelectedTransactionData:Object
        },
        emits:["editTransactionClose", "editTransactionValidated"],
        data(){
            return{
                transactionData:{},
                transactionValid:null,
                showTransactionDate:false,
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            SelectedTransactionData:{
                immediate:true,
                deep:true,
                handler(newVal,oldVal){
                    this.transactionData = newVal;
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
                this.$emit("editTransactionClose");
            },
            onTransactionValidated(){
                this.$emit("editTransactionValidated", this.transactionData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>