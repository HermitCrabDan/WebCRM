<template>
    <div>
        <w-app block>
            <h1>Members</h1>
            <br />
            <w-notification v-model="isLoading" success bg-color="white">
                ....loading
            </w-notification>
            <w-notification v-model="isError" error bg-color="white">
                An Error Occured
            </w-notification>
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
                    >
                </member-detail>
            </div>
            <div v-else>
                <w-button @click="createNewMember">Create New Member</w-button>
                <div class="--bg pa12">
                    <w-card title="Existing Members" title-class="blue-light5--bg pa3">
                        <w-input 
                            v-model="keyword"
                            placeholder="Search any column..."
                            inner-icon-left="wi-search"
                            class="mb3"
                            style="text-align:left"
                            >
                        </w-input>
                        <w-table
                        :headers="memberListHeaders"
                        :items="memberList"
                        :loading="isLoading"
                        :sort="memberSort"
                        :filter="keywordFilter(keyword)"
                        :selectable-rows="1"
                         @row-select="selectMember($event.item)">
                        </w-table>
                    </w-card>
                </div>
            </div>
        </w-app>
    </div>
</template>

<script>
import axios from 'axios';
import NewMember from '../components/NewMember.vue';
import MemberDetail from '../components/MemberDetail.vue';

export default {
    name:"Members",
    components:{
        'new-member': NewMember,
        'member-detail': MemberDetail,
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

            isError: false,
            isLoading: false,

            newMode: false,
            editMode: false,

            apiUrl: "api/MemberData",

            selectedMember: {},

            keyword:'',
            keywordFilter: keyword => item => {
            // Concatenate all the columns into a single string for a more performant lookup.
            const allTheColumns = `${item.memberName} ${item.userId} ${item.createdBy} ${item.lastUpdatedBy}`

            // Lookup the keyword variable in the string with case-insensitive flag.
            return new RegExp(keyword, 'i').test(allTheColumns)
            },
        }
    },
    methods:{
        createNewMember(){
            this.newMode = true;
            this.editMode = false;
        },
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
                    console.log(response.data);
                    this.editMode = false;
                })
                .catch(error => {
                    console.log(error);
                    this.isError = true;
                })
                .then(() => {
                    this.isLoading = false;
                    if (!this.isError){
                        this.getList();
                    }
                });
        },
        submitNewMember(memberData){
            this.isLoading = true;
            this.isError = false;
            axios
                .post(this.apiUrl, memberData)
                .then(response => { 
                    console.log(response.data);
                    this.newMode = false;
                })
                .catch(error => {
                    console.log(error);
                    this.isError = true;
                })
                .then(() => {
                    this.isLoading = false;
                    if (!this.isError){
                        this.getList();
                    }
                });
        },
        getList(){
            this.isLoading = true;
            this.isError = false;
            axios
                .get(this.apiUrl)
                .then(response => { 
                    this.memberList = response.data; 
                    console.log(response.data);
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
        this.getList();
    }
}
</script>

<style lang="scss" scoped>

</style>