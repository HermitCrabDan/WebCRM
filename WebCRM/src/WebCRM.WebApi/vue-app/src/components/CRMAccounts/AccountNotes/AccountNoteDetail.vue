<template>
    <div>
        <w-button
            @click="closeClick" 
            sm 
            outline 
            round 
            absolute 
            icon="wi-cross" >
        </w-button>
        <w-flex justify-center fill-width>
            <w-card title="Edit Note" title-class="blue-light5--bg pa3" style="min-width:600px">
                <model-details
                    :modelData="noteData">
                </model-details>
                <br />
                <w-flex justify-center fill-width>
                    <div style="border:solid 1px silver; padding:5px; min-width:600px">
                        <w-form
                            v-model="editFormValid"
                            @success="onEditSuccess"
                            >
                            <div class="message-box">
                                <w-alert
                                    class="my0 text-light"
                                    v-if="editFormValid === false"
                                    error
                                    no-border>
                                    The form has errors.
                                </w-alert>
                            </div>
                            <w-textarea
                                label="Note Text"
                                v-model="noteData.noteText"
                                :validators="[validators.required]"
                                >
                            </w-textarea>
                            <br />
                            <div v-if="noteData.deletionDate">
                                <w-button @click="unDeleteClick">Reinstate Note</w-button>
                            </div>
                            <div v-else>
                                <div>
                                    <w-button class="my1"
                                        type="submit">
                                        Edit Note
                                    </w-button>
                                </div>
                                <br />
                                <div>
                                    <w-button @click="removeClick">Remove Note</w-button>
                                </div>
                            </div>
                        </w-form>
                    </div>
                </w-flex>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import ModelDetails from '../../ModelDetails.vue';

    export default {
        name:"AccountNoteDetail",
        components:{
            'model-details':ModelDetails,
        },
        props:{
            selectedNoteData:Object,
        },
        data(){
            return{
                noteData:{ noteText:'' },
                editFormValid:null,

                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        emits:["editValidationSuccess","accountNoteDetailClose","reinstateNoteClick","removeNoteClick"],
        watch:{
            selectedNoteData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.noteData = newVal;
                }
            }
        },
        methods:{
            closeClick(){
                this.$emit("accountNoteDetailClose");
            },
            onEditSuccess(){
                this.$emit("editValidationSuccess", this.noteData);
            },
            removeClick(){
                this.$emit("removeNoteClick", this.noteData);
            },
            unDeleteClick(){
                this.$emit("reinstateNoteClick", this.noteData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>