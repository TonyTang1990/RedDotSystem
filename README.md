# RedDotSystem
## 红点系统需求

在开始设计和编写我们需要的红点系统前，让我们先理清下红点的需求:

1. **单个红点可能受多个游戏逻辑因素影响**
2. **内层红点可以影响外层红点可以不影响外层红点**
3. **红点显示逻辑会受功能解锁和游戏数据流程影响动态生效，如果全部都全量定义初始化会导致初期太多无效红点名的定义和计算**
4. **单个红点名的逻辑常规设计会散落到各个模块代码，很零散不便于维护和修改**
5. **红点样式多种多样(e.g. 1. 纯红点 2. 纯数字红点 3. 新红点 4. 混合红点等)**
6. **同样的功能入口出现在不同地方的情况**
7. **红点结果计算的数据来源可以是客户端计算红点也可以是服务器已经算好的红点结论(有时候为了优化数据通信量，有些功能的详情会按需请求导致客户端无法直接计算红点，采取后端直接通知红点的方式)**
8. **红点分静态红点(界面上固定存在的)和动态红点(列表里数量繁多的以及根据逻辑动态创建销毁的)**
9. **数据变化会触发红点频繁逻辑运算(有时候还会触发重复运算)导致GC和时间占用**
10. **红点影响因素比较多，查询的时候缺乏可视化高效的查询手段**

## 红点系统设计

针对上面的红点需求，我通过以下设计来一一解决:

1. **采用前缀树数据结构，从红点命名上解决红点父子定义关联问题**
2. **红点运算单元采用最小单元化定义，采用组合定义方式组装红点影响因素，从而实现高度自由的红点运算逻辑组装**
3. **统一抽象红点初始化模块流程，支持按系统解锁的设计去解锁对应红点初始化模块，做到按需解锁红点名初始化功能。**
4. **红点名允许定义红点处理器统一红点名的所有相关逻辑编写流程封装，方便维护和扩展，避免相关逻辑代码零散问题。**
5. **红点运算单元支持多种显示类型定义(e.g. 1. 纯红点 2. 纯数字红点 3. 新红点 4. 混合红点等)，红点最终显示类型由所有影响他的红点运算单元计算结果组合而成(e.g. 红点运算单元1(新红点类型)+红点运算单元2(数字红点类型)=新红点类型)**
6. **采用直接复制对应红点名所有红点单元的方式(只负责添加红点单元不负责绑定，绑定数据由原始入口的红点名负责)**
7. **列表里数量过多的红点推荐采用只定义上层红点单元包含所有，内部红点显示采用自行计算的方式，列表数量不多的情况也可采用模版红点动态创建每一个列表红点名和红点单元。**
8. **非列表红点推荐全部采用静态红点预定义**
9. **根据逻辑动态创建销毁的推荐使用动态红点方式，通过逻辑成去创建和移除红点名做到同步动态红点名生命周期。**
10. **红点单元采用标脏加延迟计算+每帧限制红点单元计算数量的方式优化单帧计算过多卡顿问题。同时红点运算结果按红点运算单元为单位计算并缓存结果避免重复运算。**
11. **编写自定义EditorWindow实现红点数据全面可视化提升红点系统可维护性**

## 红点系统类说明

1. RedDotName.cs -- 红点名定义(通过前缀树表达父子关系，所有静态红点都一开始定义在这里)
2. RedDotInfo.cs -- 红点信息类
3. RedDotUnit.cs -- 红点运算单元定义(所有静态红点需要参与运算的最小单元都定义在这里)
4. RedDotUnitInfo.cs -- 红点运算单元类
5. RedDotType.cs -- 红点类型(用于支持上层各类复杂的红点显示方式 e.g. 纯红点，纯数字，新红点等)
6. RedDotModel.cs -- 红点数据层(所有的红点名信息和红点运算单元信息全部在这一层初始化)
7. RedDotManager.cs -- 红点单例管理类(提供统一的红点管理，红点运算单元计算结果缓存，红点绑定回调等流程)
8. RedDotInitilizer.cs -- 红点初始化器(负责统一红点初始化器的初始化流程)
9. FuncRDInitializer.cs -- 功能红点数据初始化器基类抽象
10. SystemRDInitializer.cs -- 系统红点初始化器抽象(支持按需解锁的红点初始化器抽象)
11. RedDotHandler.cs -- 红点处理器基类抽象(主要负责维护单个红点相关的逻辑和数据(比如红点单元初始化，绑定，解绑定，事件监听添加，解除事件监听和标脏等))
12. DynamicRedDotHandler.cs -- 泛型动态红点处理器(用于支持动态带数据的红点处理器创建)
13. RedDotUnitBindData.cs -- 红点单元绑定信息(用于记录绑定上下文数据，方便处理是否递归添加和清理操作)
14. RedDotUtilities.cs -- 红点辅助类(一些通用方法还有逻辑层的红点运算方法定义在这里)
15. GameModel.cs -- 逻辑数据层存储模拟
16. RedDotEditorWindow.cs -- 红点系统可视化窗口(方便快速可视化查看红点运行状态和相关信息)
17. RedDotStyles.cs -- 红点Editor显示Style定义
18. Trie.cs -- 前缀树(用于红点名通过字符串的形式表达出层级关系)
19. TrieNode.cs -- 前缀树节点

## 实战

由于代码部分比较多，这里就不放源代码了，直接看实战效果图，源码直接下载Github源码即可。

初始化后的红点前缀树状态:

![RedDotTriePreiview](/img/RedDotSystem/RedDotTriePreiview.PNG)

点击菜单->背包->点击增加1个当前页签的新道具，切换页签并点击操作数据增加:

![BackpackUIOperation](/img/RedDotSystem/BackpackUIOperation.PNG)

背包操作完后，主界面状态:

![MainUIAfterBackpackOperation](/img/RedDotSystem/MainUIAfterBackpackOperation.PNG)

背包操作完后，红点可视化前缀树:

![RedDotTrieAfterBackpackUIOperation](/img/RedDotSystem/RedDotTrieAfterBackpackUIOperation.PNG)

背包增加操作后，MAIN_UI_MENU红点名的红点可视化详情:

![RedDotDetailAfterBackpackOperation](/img/RedDotSystem/RedDotDetailAfterBackpackOperation.PNG)

所有红点运算单元详情:

![AllRedDotUnitInfoPreview](/img/RedDotSystem/AllRedDotUnitInfoPreview.PNG)

通过菜单->背包->点击减少1个当前页签的新道具，切换页签点击并操作数据减少:

![BackpackUIOperationAfterReduce](/img/RedDotSystem/BackpackUIOperationAfterReduce.PNG)

背包减少操作后,红点可视化前缀树:

![RedDotTriePreviewAfterBackpackReduce](/img/RedDotSystem/RedDotTriePreviewAfterBackpackReduce.PNG)

关卡系统解锁前红点初始化器信息：

![BeforeLevelUnlockRDInitializer](/img/RedDotSystem/BeforeLevelUnlockRDInitializer.PNG)

关卡系统解锁后红点初始化器信息：

![AfterLevelUnlockRDInitializer](/img/RedDotSystem/AfterLevelUnlockRDInitializer.PNG)

关卡系统解锁后的红点前缀树状态：

![AfterLevelUnlockTriePreview](/img/RedDotSystem/AfterLevelUnlockTriePreview.PNG)

添加动态活动1的前缀树状态：

![AfterAddDynamicActivity1TriePreview](/img/RedDotSystem/AfterAddDynamicActivity1TriePreview.PNG)

添加动态活动1内部红点数据界面和红点前缀树状态：

![AfterAddActivity1RedDotNumUI](/img/RedDotSystem/AfterAddActivity1RedDotNumUI.PNG)

![AfterAddActivity1RedDotNumTriePreview](/img/RedDotSystem/AfterAddActivity1RedDotNumTriePreview.PNG)

移除动态活动1主界面和红点前缀树状态：

![AfterRemoveActivity1RedDotNumMainUI](/img/RedDotSystem/AfterRemoveActivity1RedDotNumMainUI.PNG)

![AfterRemoveActivity1RedDotNumTriePreview](/img/RedDotSystem/AfterRemoveActivity1RedDotNumTriePreview.PNG)

从上面的测试可以看到，我们通过定义红点名，红点运算单元相关数据，成功的分析出了红点层级关系(利用前缀树)以及红点名与红点运算单元的组合关系。

通过编写RedDotEditorWindow成功将红点数据详情可视化的显示在了调试窗口上，通过调试窗口我们可以快速的查看所有红点名和红点运算单元的相关数据，从而实现快速的调试和查看功能。

红点逻辑层只需关心红点名和红点运算单元的定义以及红点单元标脏时机，上层显示逻辑只需要绑定对应红点名刷新显示即可。

代码比较多，这里只展示红点定义刷新的几个基本流程，详情自行查看下源码。

### 红点定义流程

1. **红点系统初始化**

   ```CS
   	// 红点系统先初始化
   	RedDotManager.Singleton.Init();
   	******
       // 红点初始化器依赖了系统解锁模块
       // 所以系统解锁模块必须在RedDotInitializer前初始化
       SystemUnlockModel.Singleton.Init();
   	// 后初始化基础红点数据
   	RedDotInitializer.Singleton.Init();
   	******
       // 确保所有游戏Model数据初始化完成再初始化红点数据Model
       // 避免标脏相同红点运算单元反复标脏触发重复计算
       RedDotModel.Singleton.Init();
   ```

2. **主界面红点初始化器初始化**

   ```CS
   /// <summary>
   /// RootRDInitializer.cs
   /// 红点根初始化器(用于确保从根部开始创建和反向清理工作)
   /// </summary>
   public class RootRDInitializer : FuncRDInitializer
   {
       /// <summary>
       /// 初始化嵌套的功能红点初始化器
       /// </summary>
       protected override void InitNestedInitializers()
       {
           base.InitNestedInitializers();
           CreateNestedInitializer<MainUIRDInitializer>();
       }
   	
       ******
   }
   ```

3. **主界面红点信息和相关红点初始化器初始化**

   ```CS
   
   /// <summary>
   /// MainUIRDInitializer.cs
   /// 主界面红点初始化器
   /// </summary>
   public class MainUIRDInitializer : FuncRDInitializer
   {
       /// <summary>
       /// 初始化嵌套的功能红点初始化器
       /// </summary>
       protected override void InitNestedInitializers()
       {
           base.InitNestedInitializers();
          ******
       }
   
       /// <summary>
       /// 初始化红点数据
       /// </summary>
       protected override void InitRedDotInfos()
       {
           ******
       }
   }
   ```

4. **带系统动态解锁的红点初始化器定义**

   ```csharp
   
   /// <summary>
   /// LevelRDInitializer.cs
   /// 关卡红点初始化器
   /// </summary>
   public class LevelRDInitializer : SystemRDInitializer
   {
       /// <summary>
       /// 系统类型
       /// </summary>
       protected override SystemType SystemType
       {
           get
           {
               return SystemType.Level;
           }
       }
   
       /// <summary>
       /// 初始化嵌套的功能红点初始化器
       /// </summary>
       protected override void InitNestedInitializers()
       {
           base.InitNestedInitializers();
           // 测试嵌套初始化器功能
           CreateNestedInitializer<PVERDInitializer>();
       }
       
       ******
   }
   ```

5. **带处理器定义的红点单元信息初始化**

   ```csharp
   /// <summary>
   /// EquipRDInitializer.cs
   /// 装备红点初始化器
   /// </summary>
   public class EquipRDInitializer : SystemRDInitializer
   {
       ******
   
       /// <summary>
       /// 初始化红点数据
       /// </summary>
       protected override void InitRedDotInfos()
       {
           AddRedDotInfo<EquipUIWearableRDH>(RedDotNames.EQUIP_UI_WEARABLE, "装备界面可穿戴红点");
           AddRedDotInfo<EquipUIUpgradableRDH>(RedDotNames.EQUIP_UI_UPGRADABLE, "装备界面可升级红点");
       }
   }
   
   /// <summary>
   /// EquipUIWearableRDH.cs
   /// 装备界面可穿戴红点处理器
   /// </summary>
   public class EquipUIWearableRDH : RedDotHandler
   {
       /// <summary>
       /// 初始化红点数据
       /// </summary>
       protected override void InitRedDotDatas()
       {
           AddBindRedDotUnit(RedDotUnit.WEARABLE_EQUIP_NUM, "可穿戴装备数", CaculateWearableEquipNum, RedDotType.NUMBER);
       }
   
       /// <summary>
       /// 添加所有事件监听
       /// </summary>
       protected override void AddAllEvents()
       {
           base.AddAllEvents();
           AddEvent(EventId.WearableEquipNumUpdate, OnWearableEquipNumUpdate);
       }
   
       /// <summary>
       /// 响应可穿戴装备数量更新
       /// </summary>
       /// <param name="param"></param>
       protected void OnWearableEquipNumUpdate(object param)
       {
           RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.WEARABLE_EQUIP_NUM);
       }
   
       /// <summary>
       /// 计算可穿戴装备数
       /// </summary>
       /// <returns></returns>
       protected int CaculateWearableEquipNum()
       {
           return EquipModel.Singleton.WearableEquipNum;
       }
   }
   ```

6. **动态活动红点事例**

   ```csharp
   /// <summary>
   /// ActivityData.cs
   /// 活动数据类
   /// </summary>
   public class ActivityData
   {
       ******
   
       /// <summary>
       /// 初始化红点信息
       /// </summary>
       protected void InitRedDotInfos()
       {
           var actRedDotEntryName = ActivityAgent.GetActEntryRedDotName(ActivityConfId);
           RedDotModel.Singleton.AddRedDotInfo(actRedDotEntryName, $"活动Id:{ActivityConfId}入口红点");
           var actRedDot1EntryName = ActivityAgent.GetActRedDot1EntryName(ActivityConfId);
           RedDotModel.Singleton.AddDynamicRedDotInfo<ActRed1NumRDH, int>(actRedDot1EntryName, $"活动Id:{ActivityConfId}红点1入口红点", ActivityConfId);
           var actRedDot2EntryName = ActivityAgent.GetActRedDot2EntryName(ActivityConfId);
           RedDotModel.Singleton.AddDynamicRedDotInfo<ActRed2NumRDH, int>(actRedDot2EntryName, $"活动Id:{ActivityConfId}红点2入口红点", ActivityConfId);
       }
   
       /// <summary>
       /// 移除所有红点信息
       /// </summary>
       protected void RemoveAllRedDotInfos()
       {
           var actRedDotEntryName = ActivityAgent.GetActEntryRedDotName(ActivityConfId);
           RedDotModel.Singleton.RemoveRedDotInfo(actRedDotEntryName);
           var actRedDot1EntryName = ActivityAgent.GetActRedDot1EntryName(ActivityConfId);
           RedDotModel.Singleton.RemoveRedDotInfo(actRedDot1EntryName);
           var actRedDot2EntryName = ActivityAgent.GetActRedDot2EntryName(ActivityConfId);
           RedDotModel.Singleton.RemoveRedDotInfo(actRedDot2EntryName);
       }
   }
   ```

7. **上层逻辑绑定红点名刷新**

   ```CS
   /// <summary>
   /// EquipUI.cs
   /// 装备UI
   /// </summary>
   public class EquipUI : BaseUI
   {
       ******
       
       /// <summary>
       /// 绑定所有红点名
       /// </summary>
       protected override void BindAllRedDotNames()
       {
           base.BindAllRedDotNames();
           WearableEquipRedDot.Init(RedDotNames.EQUIP_UI_WEARABLE);
           UpgradableEquipRedDot.Init(RedDotNames.EQUIP_UI_UPGRADABLE);
       }
   
       ******
   }
   ```

8. 上层红点组件封装**

   ```CS
   using System.Collections;
   using System.Collections.Generic;
   using UnityEngine;
   using UnityEngine.UI;
   
   /// <summary>
   /// RedDotWidget.cs
   /// 红点子组件
   /// </summary>
   public class RedDotWidget : MonoBehaviour
   {
       /// <summary>
       /// 红点图片
       /// </summary>
       [Header("红点图片")]
       public Image ImgRedDot;
   
       /// <summary>
       /// 红点文本
       /// </summary>
       [Header("红点文本")]
       public Text TxtRedDotDes;
   
       /// <summary>
       /// 红点名
       /// </summary>
       [SerializeField, Header("红点名")]
       private string mRedDotName;
   
       /// <summary>
       /// 响应销毁
       /// </summary>
       public void OnDestroy()
       {
           UnbindRedDotName();
       }
   
       /// <summary>
       /// 初始化
       /// </summary>
       /// <param name="redDotName"></param>
       public void Init(string redDotName)
       {
           BindRedDotName(redDotName);
       }
   
       /// <summary>
       /// 解绑红点名
       /// </summary>
       public void UnbindRedDotName()
       {
           if(string.IsNullOrEmpty(mRedDotName))
           {
               SetActive(false);
               return;
           }
           RedDotManager.Singleton.UnbindRedDotName(mRedDotName, OnRedDotRefresh);
           mRedDotName = null;
       }
   
       /// <summary>
       /// 绑定红点名
       /// </summary>
       /// <param name="redDotName"></param>
       public void BindRedDotName(string redDotName = null)
       {
           UnbindRedDotName();
           mRedDotName = redDotName;
           if(string.IsNullOrEmpty(mRedDotName))
           {
               SetActive(false);
               return;
           }
           RedDotManager.Singleton.BindRedDotName(mRedDotName, OnRedDotRefresh);
           RedDotManager.Singleton.TriggerRedDotNameUpdate(mRedDotName);
       }
   
       /// <summary>
       /// 红点刷新回调
       /// </summary>
       /// <param name="redDotName"></param>
       /// <param name="result"></param>
       /// <param name="redDotType"></param>
       private void OnRedDotRefresh(string redDotName, int result, RedDotType redDotType)
       {
           var resultText = RedDotUtilities.GetRedDotResultText(result, redDotType);
           SetActive(result > 0);
           SetRedDotTxt(resultText);
       }
   
       /// <summary>
       /// 设置红点显隐
       /// </summary>
       /// <param name="active"></param>
       private void SetActive(bool active)
       {
           gameObject.SetActive(active);
       }
   
       /// <summary>
       /// 设置红点描述
       /// </summary>
       /// <param name="redDotRes"></param>
       private void SetRedDotTxt(string redDotRes)
       {
           TxtRedDotDes.text = redDotRes;
       }
   }
   ```

## 动态红点支持(2026/7/10)

动态红点核心设计：

1. **动态红点名和动态红点单元都通过运行时动态拼接名字和注入**

细节代码设计修改：

**红点运算单元本质上就是一个唯一标识，最初RedDotUnit是枚举定义的唯一好处就是方便重复定义识别(编辑器即可报错)，后来考虑到支持动态红点，RedDotUnit修改成和RedDotNames一样的字符串定义方式。这样动态红点的红点名和红点运算单元取名只需组装一个唯一标识的字符串即可。**

Note:

1. **关于动态红点名和红点单元名取名里面重复问题，可以考虑现在RedDotNames.cs和RedDotUnit里先取一个对应的名字，然后使用这个名字取拼接一个唯一标识字符串(比如${{RedDotNames.*}_{uid}，%"{RedDotUnit.*}_{uid}")**

## 使用建议

1. **数量固定且不多的统统定义成静态红点名和静态红点单元的方式(预估占游戏里红点数量的70%)**
2. **数量不多且数量由配置或游戏逻辑决定的情况(比如一些多难度游戏入口(根据配置的难度数量决定))，可以使用动态红点的方式运行时定义红点名和红点单元(预估占游戏里红点数量的25%)**
3. **数量特别多且由游戏逻辑或配置决定的情况(比如背包道具红点(数量种类特别多跟拥有的道具数量挂钩))，可以使用上层统一定义一个包含所有的入口红点名和红点单元，列表内部采用常规的事件和逻辑判定红点显示(不走红点系统)(预估占游戏里红点数量的5%)**

## 博客

[红点系统](http://tonytang1990.github.io/2022/08/12/%E7%BA%A2%E7%82%B9%E7%B3%BB%E7%BB%9F/)