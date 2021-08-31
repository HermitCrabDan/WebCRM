<template>
    <div>
        <w-app block>
            <h1>Members</h1>
            <br />
            <member-manager></member-manager>
        </w-app>
    </div>
</template>

<script>
import MemberManager from '../components/Members/MemberMananger.vue';

export default {
    name:"Members",
    components:{
        'member-manager':MemberManager,
    },
}
</script>

<style lang="scss" scoped>

</style>