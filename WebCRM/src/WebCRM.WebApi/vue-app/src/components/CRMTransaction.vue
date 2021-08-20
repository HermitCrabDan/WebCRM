<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="transactionTitle" title-class="blue-light5--bg pa3" style="min-width:400px">
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
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import VueCal from 'vue-cal';

    export default {
        name: "CRMTransaction",
        components:{
            'vue-cal':VueCal
        },
        emits:["transactionValidated","transactionClose"],
        data(){
            return{
                transactionData:{},
                transactionValid:null,
                transactionTitle:'New Transaction'
            }
        },
        methods:{
            onValidationSuccess(){
                this.$emit("transactionValidated", this.transactionData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>