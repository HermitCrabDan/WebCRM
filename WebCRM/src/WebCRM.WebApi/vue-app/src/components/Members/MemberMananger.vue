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
            <new-member
                @newMemberClose="closeNewMember"
                @newMemberSuccess="submitNewMember"
                >
            </new-member>
        </div>
        <div v-else-if="editMode">
            <member-detail
                :selectedMemberData="selectedMember"
                @memberDetailClose="closeMemberDetail"
                @memberEditSuccess="updateMemberData"
                @removeMemberClick="removeMember"
                @reinstateMemberClick="reinstateMember"
                >
            </member-detail>
        </div>
        <div v-else>
            <w-button @click="newMode = true">Create New Member</w-button>
            <div class="--bg pa12">
                <w-card title="Existing Members" title-class="blue-light5--bg pa3">
                    <model-list-base
                        :dataList="memberList"
                        :headerList="memberListHeaders"
                        :sortString="memberSort"
                        @dataItemSelected="selectMember"
                        >
                    </model-list-base>
                </w-card>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import NewMember from './NewMember.vue';
    import MemberDetail from './MemberDetail.vue';
    import ModelListBase from '../ModelListBase.vue';
    import ModelValidationErrorList from '../ModelValidationErrorList.vue';
        
    export default {
        name:"MemberManager",
        components:{
            'new-member': NewMember,
            'member-detail': MemberDetail,
            'model-list-base':ModelListBase,
            'model-validation-error-list':ModelValidationErrorList,
        },
        data(){
            return {
                memberList: [],
                memberListHeaders: [
                    { label:'Member ID', key:'id', align:'left' },
                    { label:'Member Name', key:'memberName', align:'left' },
                    { label:'User ID', key:'userId', align:'left' },
                    { label:'Created by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                memberSort:'-creationDate',

                errorData:{ validationErrorMessages:[] },

                isError: false,
                isLoading: false,

                newMode: false,
                editMode: false,

                apiUrl: "api/MemberData",

                selectedMember: {},
            }
        },
        methods:{
            closeNewMember(){
                this.newMode = false;
            },
            selectMember(selectedMemberData){
                this.selectedMember = selectedMemberData;
                this.newMode = false;
                this.editMode = true;
            },
            closeMemberDetail(){
                this.editMode = false;
            },
            updateMemberData(memberData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .put(this.apiUrl, memberData)
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
                            this.loadMemberData();
                        }
                    });
            },
            submitNewMember(memberData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .post(this.apiUrl, memberData)
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
                            this.loadMemberData();
                        }
                    });
            },
            removeMember(memberData){
                this.isLoading = true;
                this.isError = false;
                axios
                    .delete(this.apiUrl + '/' + memberData.id)
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
                            this.loadMemberData();
                        }
                    })
            },
            reinstateMember(memberData){
                this.isError = false;
                this.isLoading = true;
                memberData.deletionDate = null;
                memberData.deletionBy = '';
                axios
                    .put(this.apiUrl, memberData)
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
                            this.loadMemberData();
                        }
                    });
            },
            loadMemberData(){
                this.isLoading = true;
                this.isError = false;
                axios
                    .get(this.apiUrl)
                    .then(response => { 
                        this.memberList = response.data; 
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
            this.loadMemberData();
        }
                
    }
</script>

<style lang="scss" scoped>

</style>