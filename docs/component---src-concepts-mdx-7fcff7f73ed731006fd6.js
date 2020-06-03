(window.webpackJsonp=window.webpackJsonp||[]).push([[4],{"N+Sq":function(e,t,n){"use strict";n.r(t),n.d(t,"_frontmatter",(function(){return l})),n.d(t,"default",(function(){return r}));n("5hJT"),n("W1QL"),n("K/PF"),n("t91x"),n("75LO"),n("PJhk"),n("mXGw");var a=n("/FXl"),i=n("TjRS");n("aD51");function o(){return(o=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var a in n)Object.prototype.hasOwnProperty.call(n,a)&&(e[a]=n[a])}return e}).apply(this,arguments)}var l={};void 0!==l&&l&&l===Object(l)&&Object.isExtensible(l)&&!l.hasOwnProperty("__filemeta")&&Object.defineProperty(l,"__filemeta",{configurable:!0,value:{name:"_frontmatter",filename:"src/concepts.mdx"}});var c={_frontmatter:l},s=i.a;function r(e){var t=e.components,n=function(e,t){if(null==e)return{};var n,a,i={},o=Object.keys(e);for(a=0;a<o.length;a++)n=o[a],t.indexOf(n)>=0||(i[n]=e[n]);return i}(e,["components"]);return Object(a.b)(s,o({},c,n,{components:t,mdxType:"MDXLayout"}),Object(a.b)("h1",{id:"concepts"},"Concepts"),Object(a.b)("h2",{id:"fattest"},"FatTest"),Object(a.b)("pre",null,Object(a.b)("code",o({parentName:"pre"},{className:"language-typescript"}),'using Yontech.Fat;\n\npublic class HomePageTests : FatTest\n{\n    HomePage homePage { get; set; }\n\n    public override void BeforeEachTestCase()\n    {\n        WebBrowser.Navigate("https://example.com/home");\n    }\n\n    public void Test_should_have_welcome_message()\n    {\n        homePage.WelcomeMessage.ShouldContainText("Welcome");\n    }\n\n    public void Test_header_should_contain_login_button()\n    {\n        // ... \n    }\n}\n')),Object(a.b)("p",null,"To write test cases you should:"),Object(a.b)("ul",null,Object(a.b)("li",{parentName:"ul"},"Create a class which inherits from ",Object(a.b)("inlineCode",{parentName:"li"},"FatTest")),Object(a.b)("li",{parentName:"ul"},"Create public methods which start with ",Object(a.b)("inlineCode",{parentName:"li"},"Test")," keyword. Each one of this methods will be considered a test case"),Object(a.b)("li",{parentName:"ul"},"Override lifecycle events to execute actions before or after each test case",Object(a.b)("ul",{parentName:"li"},Object(a.b)("li",{parentName:"ul"},Object(a.b)("inlineCode",{parentName:"li"},"BeforeEachTestCase")),Object(a.b)("li",{parentName:"ul"},Object(a.b)("inlineCode",{parentName:"li"},"BeforeAllTestCases")),Object(a.b)("li",{parentName:"ul"},Object(a.b)("inlineCode",{parentName:"li"},"AfterEachTestCase")),Object(a.b)("li",{parentName:"ul"},Object(a.b)("inlineCode",{parentName:"li"},"AfterAllTestCases")))),Object(a.b)("li",{parentName:"ul"},"Add properties for ",Object(a.b)("a",o({parentName:"li"},{href:"#fatpage"}),"FatPage"),"s, ",Object(a.b)("a",o({parentName:"li"},{href:"#fatpagesection"}),"FatPageSection"),"s, ",Object(a.b)("a",o({parentName:"li"},{href:"#fatflow"}),"FatFlow"),"s that will be used in test cases",Object(a.b)("blockquote",{parentName:"li"},Object(a.b)("p",{parentName:"blockquote"},"FatFramework will inject those properties for you at runtime so you don't need to care about constructing them.")))),Object(a.b)("h2",{id:"fatpage"},"FatPage"),Object(a.b)("pre",null,Object(a.b)("code",o({parentName:"pre"},{className:"language-typescript"}),'using Yontech.Fat;\nusing Yontech.Fat.WebControls;\n\npublic class HomePage : FatPage\n{\n    public ITextControl WelcomeMessage => _.Text("#welcome-message-id");\n\n    public IButtonControl LoginButton => _.Button(".some-class .login-btn");\n}\n')),Object(a.b)("p",null,"The role of ",Object(a.b)("inlineCode",{parentName:"p"},"FatPage")," is to have all elements of a page grouped in a single place so that they can be reused in multiple TestCases.\nEach page element will have defined:"),Object(a.b)("ul",null,Object(a.b)("li",{parentName:"ul"},Object(a.b)("strong",{parentName:"li"},"Type"),". It can be Text, TextBox, Button, Dropdown, Link, ... or custom components"),Object(a.b)("li",{parentName:"ul"},Object(a.b)("strong",{parentName:"li"},"Name"),". We recommend to include the type of the component inside the name. eg. instead of ",Object(a.b)("em",{parentName:"li"},"Login")," use ",Object(a.b)("em",{parentName:"li"},"LoginButton")),Object(a.b)("li",{parentName:"ul"},Object(a.b)("strong",{parentName:"li"},"CSS selector")," used to identify the component on the page ")),Object(a.b)("p",null,"These are being linked together by the builtin ",Object(a.b)("a",o({parentName:"p"},{href:"#controlfinder-aka-_"}),"ControlFinder")," which is available using the ",Object(a.b)("inlineCode",{parentName:"p"},"_")," (underscore) sign. "),Object(a.b)("h2",{id:"fatpagesection"},"FatPageSection"),Object(a.b)("p",null,"It is very similar to ",Object(a.b)("a",o({parentName:"p"},{href:"#fatpage"}),"FatPage"),", the difference between these two is semantically. The difference is that instead of ",Object(a.b)("inlineCode",{parentName:"p"},"FatPage")," you should use ",Object(a.b)("inlineCode",{parentName:"p"},"FatPageSection"),"."),Object(a.b)("p",null,"Use ",Object(a.b)("inlineCode",{parentName:"p"},"FatPage")," when elements that you want to define are inside a page (which is usually linked to a URL)."),Object(a.b)("p",null,"Use ",Object(a.b)("inlineCode",{parentName:"p"},"FatPageSection")," when you want to define elements that not linked to a URL, for example Header, Footer, Menu, Modals, Confirmation popups."),Object(a.b)("h2",{id:"fatcustomcomponent"},"FatCustomComponent"),Object(a.b)("pre",null,Object(a.b)("code",o({parentName:"pre"},{className:"language-typescript"}),'using Yontech.Fat;\nusing Yontech.Fat.WebControls;\n\npublic class ArticleItem : FatCustomComponent\n{\n    public ILinkControl UserNameLink => _.Link(".author");\n    public ITextControl PublishedDateText => _.Text("span.date");\n    public ITextControl TitleText => _.Text(".preview-link h1");\n}\n\n// This is how ArticleItem custom component can be used\npublic class ArticlesPage : FatPage\n{\n    public ArticleItem SelectedArticle => _.Custom<ArticleItem>(".article-item.selected");\n\n    public ArticleItem ArticleAtPosition(int index)\n    {\n        var articles = _.CustomList<ArticleItem>(".article-item");\n        return articles.ElementAt(index);\n    }\n}\n\n// Now let\'s see it in action in a test\npublic class ArticlePageTests : FatTest\n{\n    ArticlesPage articlesPage { get; set; }\n\n    public void Test_first_article_should_has_valid_title()\n    {\n        articlesPage.ArticleAtPosition(0).TitleText.ShouldContainText("Have you been to Romania?");\n    }\n\n    public void Test_click_on_username_for_selected_article()\n    {\n        articlesPage.SelectedArticle.UserNameLink.Click();\n    }\n}\n')),Object(a.b)("blockquote",null,Object(a.b)("p",{parentName:"blockquote"},"Custom Components is a powerful concept in FatFramework. We recommend creating one for each Component that developer did in their source code. This way, every time they change one component you will know what needs to be adjusted in Fat project.")),Object(a.b)("h2",{id:"fatflow"},"FatFlow"),Object(a.b)("pre",null,Object(a.b)("code",o({parentName:"pre"},{className:"language-typescript"}),'using Yontech.Fat;\n\npublic class SignInFlows : FatFlow\n{\n    SignInPage signInPage { get; set; }\n\n    public void Login(string username, string password)\n    {\n        signInPage.EmailTextInput.ClearAndTypeKeys(username);\n        signInPage.PasswordTextInput.ClearAndTypeKeys(password);\n\n        signInPage.SignInButton.Click();\n    }\n}\n\n// This is an example of how it can be used\npublic class ProfilePageTests: FatTest \n{\n    SingInFlows signInFlows { get; set; }\n\n    public void Test_something()\n    {\n        signInFlows.Login("john.snow", "hold-the-door");\n\n        // ...\n    }\n\n    // or it can be used in lifecycle events like\n    public override void BeforeEachTestCase()\n    {\n        WebBrowser.Navigate("https://example.com/login");\n        signInFlows.Login("john.snow", "hold-the-door");\n    }\n}\n\n')),Object(a.b)("h2",{id:"controlfinder-aka-_"},"ControlFinder (aka _)"),Object(a.b)("h3",{id:"the-api-to-search-for-a-single-element"},"The API to search for a single element"),Object(a.b)("ul",null,Object(a.b)("li",{parentName:"ul"},"Text"),Object(a.b)("li",{parentName:"ul"},"TextBox"),Object(a.b)("li",{parentName:"ul"},"Button"),Object(a.b)("li",{parentName:"ul"},"Link"),Object(a.b)("li",{parentName:"ul"},"RadioButton"),Object(a.b)("li",{parentName:"ul"},"Checkbox"),Object(a.b)("li",{parentName:"ul"},"Dropdown"),Object(a.b)("li",{parentName:"ul"},"Custom")),Object(a.b)("h3",{id:"the-api-to-search-list-of-elements"},"The API to search list of elements"),Object(a.b)("ul",null,Object(a.b)("li",{parentName:"ul"},"TextList"),Object(a.b)("li",{parentName:"ul"},"TextBoxList"),Object(a.b)("li",{parentName:"ul"},"ButtonList"),Object(a.b)("li",{parentName:"ul"},"LinkList"),Object(a.b)("li",{parentName:"ul"},"CustomList")))}void 0!==r&&r&&r===Object(r)&&Object.isExtensible(r)&&!r.hasOwnProperty("__filemeta")&&Object.defineProperty(r,"__filemeta",{configurable:!0,value:{name:"MDXContent",filename:"src/concepts.mdx"}}),r.isMDXComponent=!0}}]);
//# sourceMappingURL=component---src-concepts-mdx-7fcff7f73ed731006fd6.js.map