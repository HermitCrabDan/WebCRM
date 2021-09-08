<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Edit Expense" title-class="blue-light5--bg pa3" style="min-width:600px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="expenseValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-drawer
                    v-model="showExpenseDate"
                    :right="true"
                    >
                    <w-button 
                        @click="showExpenseDate = false" 
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
                                @cell-focus="setExpenseDate($event)"
                                style="max-width: 270px;height: 290px">
                            </vue-cal>
                        </w-flex>
                    </div>
                </w-drawer>
                <model-details
                    :modelData="expenseData">
                </model-details>
                <w-form
                    v-model="expenseValid"
                    @success="onExpenseValidated">
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
                            label="Expense Date"
                            v-model="expenseData.expenseDateString"
                            :validators="[validators.required]"
                            readonly
                            >
                        </w-input>
                        <w-button
                            @click="showExpenseDate = true"
                            >
                            Set Date
                        </w-button>
                        </w-flex>
                    </div>
                    <br />
                    <div>
                        <w-input
                            label="Expense Amount"
                            v-model="expenseData.expenseAmount"
                            :validators="[validators.required]"
                            >
                        </w-input>
                    </div>
                    <br />
                    <br />
                    <div v-if="contractData.deletionDate">
                        <w-button @click="unDeleteClick">Reinstate Expense</w-button>
                    </div>
                    <div v-else>
                        <div>
                            <w-button class="my1"
                                type="submit">
                                Edit Expense
                            </w-button>
                        </div>
                        <br />
                        <div>
                            <w-button @click="removeClick">Remove Expense</w-button>
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
        name:"ContractExpenseDetail",
        components:{
            'vue-cal':VueCal,
            'model-details':ModelDetails,
        },
        props:{
            SelectedExpenseData:Object,
        },
        emits:["editExpenseValidated","closeExpenseDetails","removeExpenseClick","reinstateExpenseClick"],
        data(){
            return{
                expenseData:{},
                showExpenseDate:false,
                expenseValid:null,
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            SelectedExpenseData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.expenseData = newVal;
                }
            }
        },
        methods:{
            closeClick(){
                this.$emit("closeExpenseDetails");
            },
            onExpenseValidated(){
                this.$emit("editExpenseValidated", this.expenseData);
            },
            setExpenseDate(selectedDate){
                this.expenseData.expenseDate = selectedDate;
                this.expenseData.expenseDateString = selectedDate.format('MM-DD-YYYY'); 
                this.showExpenseDate = false;
            },
            unDeleteClick(){
                this.$emit("reinstateExpenseClick", this.expenseData);
            },
            removeClick(){
                this.$emit("removeExpenseClick", this.expenseData);
            }
        },
    }
</script>

<style lang="scss" scoped>

</style>