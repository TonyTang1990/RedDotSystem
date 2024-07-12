# RedDotSystem
## 红点系统需求

在开始设计和编写我们需要的红点系统前，让我们先理清下红点的需求:

1. **单个红点可能受多个游戏逻辑因素影响**
2. **内层红点可以影响外层红点可以不影响外层红点**
3. **红点显示逻辑会受功能解锁和游戏数据流程影响**
4. **红点样式多种多样(e.g. 1. 纯红点 2. 纯数字红点 3. 新红点 4. 混合红点等)**
5. **红点结果计算的数据来源可以是客户端计算红点也可以是服务器已经算好的红点结论(有时候为了优化数据通信量，有些功能的详情会按需请求导致客户端无法直接计算红点，采取后端直接通知红点的方式)**
6. **红点分静态红点(界面上固定存在的)和动态红点(列表里数量繁多的)**
7. **数据变化会触发红点频繁逻辑运算(有时候还会触发重复运算)导致GC和时间占用**
8. **红点影响因素比较多，查询的时候缺乏可视化高效的查询手段**

## 红点系统设计

针对上面的红点需求，我通过以下设计来一一解决:

1. **采用前缀树数据结构，从红点命名上解决红点父子定义关联问题**
2. **红点运算单元采用最小单元化定义，采用组合定义方式组装红点影响因素，从而实现高度自由的红点运算逻辑组装**
3. **红点运算单元作为红点影响显示的最小单元，每一个都会对应逻辑层面的一个计算代码，从而实现和逻辑层关联实现自定义解锁和计算方式**
4. **红点运算结果按红点运算单元为单位，采用标脏加延迟计算的方式避免重复运算和结果缓存**
5. **红点运算单元支持多种显示类型定义(e.g. 1. 纯红点 2. 纯数字红点 3. 新红点 4. 混合红点等)，红点最终显示类型由所有影响他的红点运算单元计算结果组合而成(e.g. 红点运算单元1(新红点类型)+红点运算单元2(数字红点类型)=新红点类型)**
6. **除了滚动列表里数量过多的红点采用界面上自行计算的方式，其他红点全部采用静态红点预定义的方式，全部提前定义好红点名以及红点运算单元组成和父子关系等数据**
7. **编写自定义EditorWindow实现红点数据全面可视化提升红点系统可维护性**

## 红点系统类说明

1. RedDotName.cs -- 红点名定义(通过前缀树表达父子关系，所有静态红点都一开始定义在这里)
2. RedDotInfo.cs -- 红点信息类
3. RedDotUnit.cs -- 红点运算单元枚举定义(所有静态红点需要参与运算的最小单元都定义在这里)
4. RedDotUnitInfo.cs -- 红点运算单元类
5. RedDotType.cs -- 红点类型(用于支持上层各类复杂的红点显示方式 e.g. 纯红点，纯数字，新红点等)
6. RedDotModel.cs -- 红点数据层(所有的红点名信息和红点运算单元信息全部在这一层初始化)
7. RedDotManager.cs -- 红点单例管理类(提供统一的红点管理，红点运算单元计算结果缓存，红点绑定回调等流程)
8. RedDotUtilities.cs -- 红点辅助类(一些通用方法还有逻辑层的红点运算方法定义在这里)
9. GameModel.cs -- 逻辑数据层存储模拟
10. RedDotEditorWindow.cs -- 红点系统可视化窗口(方便快速可视化查看红点运行状态和相关信息)
11. RedDotStyles.cs -- 红点Editor显示Style定义
12. Trie.cs -- 前缀树(用于红点名通过字符串的形式表达出层级关系)
13. TrieNode.cs -- 前缀树节点

## 实战

由于代码部分比较多，这里就不放源代码了，直接看实战效果图，源码直接下载Github源码即可。

初始化后的红点前缀树状态:

![RedDotTriePreiview](/img/RedDotSystem/RedDotTriePreiview.PNG)

点击标记功能1新按钮后:

![RedDotTriePreiviewAfterMarkFunc1New](/img/RedDotSystem/RedDotTriePreiviewAfterMarkFunc1New.PNG)

点击菜单->背包->点击增加1个当前页签的新道具，切换页签并点击操作数据增加:

![BackpackUIOperation](/img/RedDotSystem/BackpackUIOperation.PNG)

背包操作完后，主界面状态:

![MainUIAfterBackpackOperation](/img/RedDotSystem/MainUIAfterBackpackOperation.PNG)

背包操作完后，红点可视化前缀树:

![RedDotTrieAfterBackpackUIOperation](/img/RedDotSystem/RedDotTrieAfterBackpackUIOperation.PNG)

背包增加操作后，MAIN_UI_MENU红点名的红点可视化详情:

![RedDotDetailAfterBackpackOperation](/img/RedDotSystem/RedDotDetailAfterBackpackOperation.PNG)

所有红点运算单元详情:

![AllRedDotUnitInfoPreview0](/img/RedDotSystem/AllRedDotUnitInfoPreview.PNG)

通过菜单->背包->点击减少1个当前页签的新道具，切换页签点击并操作数据减少:

![BackpackUIOperationAfterReduce](/img/RedDotSystem/BackpackUIOperationAfterReduce.PNG)

背包减少操作后,红点可视化前缀树:

![RedDotTriePreviewAfterBackpackReduce](/img/RedDotSystem/RedDotTriePreviewAfterBackpackReduce.PNG)

从上面的测试可以看到，我们通过定义红点名，红点运算单元相关数据，成功的分析出了红点层级关系(利用前缀树)以及红点名与红点运算单元的组合关系。

通过编写RedDotEditorWindow成功将红点数据详情可视化的显示在了调试窗口上，通过调试窗口我们可以快速的查看所有红点名和红点运算单元的相关数据，从而实现快速的调试和查看功能。

上层逻辑只需关心红点名和红点运算单元的定义以及红点名在逻辑层的绑定刷新即可。

代码比较多，这里只展示红点定义刷新的几个基本流程，详情自行查看下源码。

### 红点定义流程

1. **红点系统初始化**

   ```CS
           RedDotModel.Singleton.Init();
           RedDotManager.Singleton.Init();
           // 所有数据初始化完成后触发一次红点运算单元计算
           RedDotManager.Singleton.DoAllRedDotUnitCaculate();
   ```

2. **定义红点运算单元并初始化**

   ```CS
   AddRedDotUnitInfo(RedDotUnit.NEW_FUNC1, "动态新功能1解锁", RedDotUtilities.CaculateNewFunc1, RedDotType.NEW);
   ******
   ```

3. **定义红点名以及对应红点运算单元组成并初始化**

   ```CS
           RedDotInfo redDotInfo;
           redDotInfo = AddRedDotInfo(RedDotNames.MAIN_UI_NEW_FUNC1, "主界面新功能1红点");
           redDotInfo.AddRedDotUnit(RedDotUnit.NEW_FUNC1);
   *******
   ```

4. **上层逻辑编写新红点运算单元的逻辑计算回调**

   ```CS
   /// <summary>
   /// 计算主界面动态新功能1解锁
   /// </summary>
   /// <returns></returns>
   public static int CaculateNewFunc1()
   {
       return GameModel.Singleton.NewFunc1 ? 1 : 0;
   }
   ```

5. **上层逻辑绑定红点名刷新**

   ```CS
   
     RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIN_UI_NEW_FUNC1, OnRedDotRefresh);
   
   /// <summary>
   /// 响应红点刷新
   /// </summary>
   /// <param name="redDotName"></param>
   /// <param name="result"></param>
   /// <param name="redDotType"></param>
   private void OnRedDotRefresh(string redDotName, int result, RedDotType redDotType)
   {
       var resultText = RedDotUtilities.GetRedDotResultText(result, redDotType);
       if (string.Equals(redDotName, RedDotNames.MAIN_UI_MENU))
       {
           MenuRedDot.SetActive(result > 0);
           MenuRedDot.SetRedDotTxt(resultText);
       }
       else if (string.Equals(redDotName, RedDotNames.MAIN_UI_NEW_FUNC1))
       {
           DynamicFunc1RedDot.SetActive(result > 0);
           DynamicFunc1RedDot.SetRedDotTxt(resultText);
       }
       ******
   }
   ```

6. **上层逻辑首次初始化红点数据**

   ```CS
           (int result, RedDotType redDotType) redDotNameResult;
           redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIN_UI_MENU);
           OnRedDotRefresh(RedDotNames.MAIN_UI_MENU, redDotNameResult.result, redDotNameResult.redDotType);
   ```

7. **上层逻辑触发红点名或红点运算单元标脏后，等待红点系统统一触发计算并回调**

   ```CS
   /// <summary>
   /// 设置主界面动态新功能1是否解锁
   /// </summary>
   /// <param name="newFunc1"></param>
   public void SetNewFunc1(bool newFunc1)
   {
       if(NewFunc1 != newFunc1)
       {
           NewFunc1 = newFunc1;
           RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_FUNC1);
       }
   }
   ```

## 博客

[红点系统](http://tonytang1990.github.io/2022/08/12/%E7%BA%A2%E7%82%B9%E7%B3%BB%E7%BB%9F/)