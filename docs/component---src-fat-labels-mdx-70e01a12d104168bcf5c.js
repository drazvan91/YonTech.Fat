(window.webpackJsonp=window.webpackJsonp||[]).push([[8],{gDWP:function(e,t,n){"use strict";n.r(t),n.d(t,"_frontmatter",(function(){return i})),n.d(t,"default",(function(){return o}));n("5hJT"),n("W1QL"),n("K/PF"),n("t91x"),n("75LO"),n("PJhk"),n("mXGw");var a=n("/FXl"),l=n("TjRS");n("aD51");function s(){return(s=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var a in n)Object.prototype.hasOwnProperty.call(n,a)&&(e[a]=n[a])}return e}).apply(this,arguments)}var i={};void 0!==i&&i&&i===Object(i)&&Object.isExtensible(i)&&!i.hasOwnProperty("__filemeta")&&Object.defineProperty(i,"__filemeta",{configurable:!0,value:{name:"_frontmatter",filename:"src/fat-labels.mdx"}});var b={_frontmatter:i},r=l.a;function o(e){var t=e.components,n=function(e,t){if(null==e)return{};var n,a,l={},s=Object.keys(e);for(a=0;a<s.length;a++)n=s[a],t.indexOf(n)>=0||(l[n]=e[n]);return l}(e,["components"]);return Object(a.b)(r,s({},b,n,{components:t,mdxType:"MDXLayout"}),Object(a.b)("h1",{id:"fat-label"},"Fat Label"),Object(a.b)("h2",{id:"defining-labels-on-a-method"},"Defining labels on a method"),Object(a.b)("pre",null,Object(a.b)("code",s({parentName:"pre"},{className:"language-typescript"}),'using Yontech.Fat;\nusing Yontech.Fat.Labels;\n\n// ...\n\npublic class MyTestCases : FatTest\n{\n    [FatLabel("my-label")]\n    public void Test_this_has_a_label()\n    {\n    }\n\n    public void Test_this_does_not_have_a_label()\n    {\n    }\n}\n\n')),Object(a.b)("h2",{id:"defining-labels-on-all-tests-within-a-fattest-class"},"Defining labels on all tests within a FatTest class"),Object(a.b)("p",null,"Defining FatLabel on the class is like defining for all tests within that class"),Object(a.b)("pre",null,Object(a.b)("code",s({parentName:"pre"},{className:"language-typescript"}),'using Yontech.Fat;\nusing Yontech.Fat.Labels;\n\n// ...\n\n[FatLabel("my-label")]\npublic class MyTestCases : FatTest\n{\n    public void Test_this_is_smoke_test()\n    {\n    }\n\n    public void Test_another_test()\n    {\n    }\n}\n\n')),Object(a.b)("h2",{id:"filter-using-dotnet-test"},"Filter using dotnet test"),Object(a.b)("pre",null,Object(a.b)("code",s({parentName:"pre"},{className:"language-shell"}),'dotnet test --filter "Label=my-label"\n\n# to filter using OR statement use |\ndotnet test --filter "Label=my-label1|Label=my-label2"\n\n# to filter using AND statement use &\ndotnet test --filter "Label=my-label1&Label=my-label2"\n')),Object(a.b)("h2",{id:"filter-using-fatconfig"},"Filter using FatConfig"),Object(a.b)("pre",null,Object(a.b)("code",s({parentName:"pre"},{className:"language-typescript"}),'using Yontech.Fat.Filters;\n\npublic class MyConfig : FatConfig\n{\n    public MyConfig()\n    {\n        Browser = BrowserType.Chrome;\n        \n        // filter tests with one label\n        Filter = new LabelTestCaseFilter("my-label";\n\n        // or filter tests with at least one of these labels\n        Filter = new LabelTestCaseFilter("my-label1", "my-label2", "my-label3")\n    }\n}\n\n')),Object(a.b)("h2",{id:"predefined-labels"},"Predefined Labels"),Object(a.b)("p",null,"There are two predefined labels that could be handy:"),Object(a.b)("ul",null,Object(a.b)("li",{parentName:"ul"},Object(a.b)("inlineCode",{parentName:"li"},"SmokeTest")," which is the same as ",Object(a.b)("inlineCode",{parentName:"li"},'FatLabel("smoke-test")')),Object(a.b)("li",{parentName:"ul"},Object(a.b)("inlineCode",{parentName:"li"},"RegressionTest")," which is the same as ",Object(a.b)("inlineCode",{parentName:"li"},'FatLabel("regression-test")'))),Object(a.b)("pre",null,Object(a.b)("code",s({parentName:"pre"},{className:"language-typescript"}),"using Yontech.Fat;\nusing Yontech.Fat.Labels;\n\n// ...\n\npublic class MyTestCases : FatTest\n{\n    [SmokeTest]\n    [RegressionTest]\n    public void Test_this_is_both()\n    {\n    }\n\n    [SmokeTest]\n    public void Test_this_is_only_smoke_test()\n    {\n    }\n}\n\n")))}void 0!==o&&o&&o===Object(o)&&Object.isExtensible(o)&&!o.hasOwnProperty("__filemeta")&&Object.defineProperty(o,"__filemeta",{configurable:!0,value:{name:"MDXContent",filename:"src/fat-labels.mdx"}}),o.isMDXComponent=!0}}]);
//# sourceMappingURL=component---src-fat-labels-mdx-70e01a12d104168bcf5c.js.map