<script>
    import Router from "svelte-spa-router";
    import SubPage from "./SubPage.svelte";
    export let params = {}; params = params;
    
    let prefix = "/my-page";
    const routes = {
        "/sub1": SubPage,
    };
</script>

<div>This is my simple page</div>
<div>
    <!-- We are at {currentPagePrefix} -->
</div>

<div>
    <h2>navlinks</h2>
    <div>
        <span>
            <a href="#{prefix}">this-root</a>
        </span>
        <span>
            <a href="#{prefix}/sub1">sub-1</a>
        </span>
    </div>
</div>

<div>
    <h2>sub page below</h2>
    <div>
        <Router {routes} {prefix} />
    </div>
</div>
