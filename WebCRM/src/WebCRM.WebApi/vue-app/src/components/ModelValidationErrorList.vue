<template>
    <div>
        <div v-if="errorList.length > 0">
            <w-list
                :items="errorList"
                color="red"
                icon="wi-cross"
                >
            </w-list>
        </div>
    </div>
</template>

<script>
    export default {
        name:"ModelValidationErrorList",
        props:{
            errorList: Array
        },
    }
</script>

<style lang="scss" scoped>

</style>