<template>
    <div>
        <div style="text-align:left">
            <w-checkbox
                v-model="showDeleted"
                @input="updateViewableData">
                Show Deleted Items
            </w-checkbox>
        </div>
        <br />
        <w-input 
            v-model="keyword"
            placeholder="Search any column..."
            inner-icon-left="wi-search"
            class="mb3"
            style="text-align:left"
            >
        </w-input>
        <w-table
            :headers="headerDataList"
            :items="viewableData"
            :selectable-rows="1"
            :sort="dataSort"
            :filter="keywordFilter(keyword)"
            @row-select="selectDataItem($event.item)">
        </w-table>
    </div>
</template>

<script>
    export default {
        name:"ModelListBase",
        props:{
            dataList:Array,
            headerList:Array,
            sortString:String,
        },
        emits:["dataItemSelected"],
        data(){
            return{
                listData:[],
                viewableData:[],
                headerDataList:[],
                dataSort:'',
                showDeleted:false,
                
                keyword:'',
                keywordFilter: keyword => item => {
                    // Concatenate all the columns into a single string for a more performant lookup.
                    const allTheColumns = `${item.id} ${item.contractName} ${item.memberName} ${item.accountName} ${item.lastUpdatedDateString} ${item.creationDateString} ${item.createdBy} ${item.lastUpdatedBy}`
                    // Lookup the keyword variable in the string with case-insensitive flag.
                    return new RegExp(keyword, 'i').test(allTheColumns)
                },
            }
        },
        methods:{
            updateViewableData(){
                this.viewableData = [];
                this.listData.forEach(element => {
                    if((!this.showDeleted && !element.deletionDate)
                        || (this.showDeleted && element.deletionDate)){
                        this.viewableData.push(element);
                    }
                });
            },
            selectDataItem(selectedData){
                this.$emit("dataItemSelected", selectedData);
            }
        },
        watch:{
            dataList:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.listData = newVal;
                    this.updateViewableData();
                }
            },
            headerList:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.headerDataList = newVal;
                }
            },
            sortString:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.dataSort = newVal;
                }
            }
        },
        mounted(){
            this.updateViewableData();
        }
    }
</script>

<style lang="scss" scoped>

</style>