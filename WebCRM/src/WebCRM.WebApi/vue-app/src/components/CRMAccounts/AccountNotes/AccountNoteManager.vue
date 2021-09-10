<template>
    <div>
        <w-notification v-model="isLoading" success bg-color="white">
            ....loading
        </w-notification>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
        <div v-if="isError">
            <model-validation-error-list
                :errorList="errorData.validationErrorMessages">
            </model-validation-error-list>
        </div>
        <div v-if="newMode">
            <new-account-note
                :selectedAccountId="accountId"
                @newNoteClose="newMode = false"
                @newNoteValidated="submitNewAccountNote"
                >
            </new-account-note>
        </div>
        <div v-else-if="editMode">
            <account-note-detail
                :selectedNoteData="selectedAccountNoteData"
                @editValidationSuccess="onEditValidationSuccess"
                @accountNoteDetailClose="closeNoteDetail"
                @reinstateNoteClick="reinstateAccountNote"
                @removeNoteClick="removeAccoutNote"
                >
            </account-note-detail>
        </div>
        <div v-else>
            <w-button @click="newMode = true">Create New Note</w-button>
            <div class="--bg pa12" style="min-width:600px; max-width:1000px;">
                <w-card title="Existing Notes" title-class="blue-light5--bg pa3">
                    <model-list-base
                        :dataList="accountNotes"
                        :headerList="accountNoteHeaders"
                        :sortString="accountNoteSort"
                        @dataItemSelected="selectAccountNote"
                        >
                    </model-list-base>
                </w-card>
            </div>
        </div>

    </div>
</template>

<script>
    import axios from 'axios';
    import ModelListBase from '../../ModelListBase.vue';
    import ModelValidationErrorList from '../../ModelValidationErrorList.vue';
    import NewAccountNote from './NewAccountNote.vue';
    import AccountNoteDetail from './AccountNoteDetail.vue';

    export default {
        name:"AccountNoteManager",
        props:{
            selectedAccountId:Number,
        },
        components:{
            'model-list-base':ModelListBase,
            'new-account-note':NewAccountNote,
            'account-note-detail':AccountNoteDetail,
            'model-validation-error-list':ModelValidationErrorList,
        },
        data(){
            return {
                accountId:0,
                accountNotes:[],
                accountNoteHeaders:[
                    { label:'Note', key:'noteText', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                accountNoteSort:'-lastUpdatedDate',

                newMode:false,
                editMode:false,

                apiUrl:'api/AccountNoteData',
                isError:false,
                isLoading:false,
                errorData:{validationErrorMessages:[]},
                selectedAccountNoteData:{},
            }
        },
        watch:{
            selectedAccountId:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.accountId = newVal;
                }
            }
        },
        methods:{
            loadAccountNotes(){
                if (this.accountId > 0){
                    this.isError = false;
                    this.isLoading = false;
                    axios
                        .get(this.apiUrl + '/' + this.accountId)
                        .then(response => {
                            this.accountNotes = response.data;
                        })
                        .catch(error => {
                            this.errorData = error.response.data;
                            this.isError = true;
                        })
                        .then(() => {
                            this.isLoading = false;
                        });
                }
            },
            submitNewAccountNote(noteData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .post(this.apiUrl, noteData)
                    .then(response => { 
                        this.errorData = response.data;
                    })
                    .catch(error => { 
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() => { 
                        this.isLoading = false; 
                        if (!this.isError){
                            this.newMode = false;
                            this.loadAccountNotes();
                        }
                    });
            },
            onEditValidationSuccess(noteData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .put(this.apiUrl, noteData)
                    .then(response => { 
                        this.errorData = response.data;
                    })
                    .catch(error => { 
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() => { 
                        this.isLoading = false;
                        if (!this.isError){
                            this.editMode = false;
                            this.loadAccountNotes();
                        }
                    });
            },
            reinstateAccountNote(noteData){
                this.isError = false;
                this.isLoading = true;
                noteData.deletionDate = null;
                noteData.deletionBy = '';
                axios
                    .put(this.apiUrl, noteData)
                    .then(response => {
                        this.errorData = response.data;
                    })
                    .catch(error => {
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() =>{
                        this.isLoading = false;
                        if (!this.isError){
                            this.editMode = false;
                            this.loadAccountNotes();
                        }
                    });
            },
            removeAccoutNote(noteData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .delete(this.apiUrl + '/' + noteData.id)
                    .then(response => {
                        this.errorData = response.data;
                    })
                    .catch(error => {
                        this.errorData = error.response.data;
                        this.isError = true;
                    })
                    .then(() => {
                        this.isLoading = false;
                        if (!this.isError){
                            this.editMode = false;
                            this.loadAccountNotes();
                        }
                    });
            },
            selectAccountNote(noteData){
                this.newMode = false;
                this.editMode = true;
                this.selectedAccountNoteData = noteData;
            },
            closeNoteDetail(){
                this.editMode = false;
                this.loadAccountNotes();
            },
        },
        mounted(){
            this.loadAccountNotes();
        }
    }
</script>

<style lang="scss" scoped>

</style>