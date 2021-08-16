<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Edit Account" title-class="blue-light5--bg pa3" style="min-width:400px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="editFormValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-form
                    v-model="editFormValid"
                    @success="onEditSuccess"
                    >
                    <w-button
                        @click="accountDetailClose" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <w-input
                        label="Account Name"
                        :value="selectedAccountData.accountName"
                        :validators="[validators.required]"
                        @input="$emit('update:selectedAccountData.accountName', $event.target.value)"
                        >
                    </w-input>
                    <w-button
                        class="my1"
                        type="submit"
                        >
                        Edit Account
                    </w-button>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    export default {
        name:"CRMAccountDetail",
        props:{
            selectedAccountData: Object
        },
        emits: ["editValidationSuccess", "accountDetailClose"],
        data(){
            return {
                editFormValid: null,
                validators: {
                    required: value => !!value || 'This field is required',
                },

            }
        },
        methods:{
            onEditSuccess(){
                this.$emit("editValidationSuccess");
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>