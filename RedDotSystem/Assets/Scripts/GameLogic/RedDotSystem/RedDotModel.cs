/*
 * Description:             RedDotModel.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotModel.cs
/// 红点数据单例类
/// </summary>
public class RedDotModel : SingletonTemplate<RedDotModel>
{
    /// <summary>
    /// 红点名和红点信息Map<红点名, 红点信息>
    /// </summary>
    private Dictionary<string, RedDotInfo> mRedDotInfoMap;

    /// <summary>
    /// 红点前缀树
    /// </summary>
    public Trie RedDotTrie
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点运算单元信息Map<红点运算单元名, 红点运算单元信息>
    /// </summary>
    private Dictionary<RedDotUnit, RedDotUnitInfo> mRedDotUnitInfoMap;

    /// <summary>
    /// 红点名和红点运算单元名列表<红点名, 红点运算单元列表>
    /// Note:
    /// 穷举的好处是足够灵活
    /// 缺点是删除最里层红点运算单元需要把外层所有影响到的红点名相关红点运算单元配置删除
    /// </summary>
    private Dictionary<string, List<RedDotUnit>> mRedDotNameUnitMap;

    /// <summary>
    /// 初始化是否完成
    /// </summary>
    public bool IsInitCompelte
    {
        get;
        private set;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public RedDotModel()
    {
        IsInitCompelte = false;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (IsInitCompelte)
        {
            Debug.LogError($"请勿重复初始化!");
            return;
        }
        InitRedDotInfo();
        InitRedDotTree();
        InitRedDotUnitInfo();
        // InitRedDotNameUnitMap必须在InitRedDotUnitInfo之后调用，因为用到了前面的数据
        InitRedDotNameUnitMap();
        IsInitCompelte = true;
    }

    /// <summary>
    /// 初始化红点信息
    /// </summary>
    private void InitRedDotInfo()
    {
        mRedDotInfoMap = new Dictionary<string, RedDotInfo>();
        // 调用AddRedDotInfo添加游戏所有静态红点信息
        InitMainUIRedDotInfo();
        InitBackpackUIRedDotInfo();
        InitMailUIRedDotInfo();
        InitEquipUIRedDotInfo();
    }

    /// <summary>
    /// 初始化主界面红点信息
    /// </summary>
    private void InitMainUIRedDotInfo()
    {
        AddRedDotInfo(RedDotNames.MAIN_UI_NEW_FUNC1, "主界面新功能1红点");
        AddRedDotInfo(RedDotNames.MAIN_UI_NEW_FUNC2, "主界面新功能2红点");
        AddRedDotInfo(RedDotNames.MAIN_UI_MENU, "主界面菜单红点");
        AddRedDotInfo(RedDotNames.MAIN_UI_MAIL, "主界面邮件红点");
        AddRedDotInfo(RedDotNames.MAIN_UI_MENU_EQUIP, "主界面菜单装备红点");
        AddRedDotInfo(RedDotNames.MAIN_UI_MENU_BACKPACK, "主界面菜单背包红点");
    }

    /// <summary>
    /// 初始化背包界面红点信息
    /// </summary>
    private void InitBackpackUIRedDotInfo()
    {
        AddRedDotInfo(RedDotNames.BACKPACK_UI_ITEM_TAG, "背包界面道具页签红点");
        AddRedDotInfo(RedDotNames.BACKPACK_UI_RESOURCE_TAG, "背包界面资源页签红点");
        AddRedDotInfo(RedDotNames.BACKPACK_UI_EQUIP_TAG, "背包界面装备页签红点");
    }

    /// <summary>
    /// 初始化邮件界面红点信息
    /// </summary>
    private void InitMailUIRedDotInfo()
    {
        AddRedDotInfo(RedDotNames.MAIL_UI_PUBLIC_MAIL, "邮件界面公共邮件红点");
        AddRedDotInfo(RedDotNames.MAIL_UI_BATTLE_MAIL, "邮件界面战斗邮件红点");
        AddRedDotInfo(RedDotNames.MAIL_UI_OTHER_MAIL, "邮件界面其他邮件红点");
    }

    /// <summary>
    /// 初始化装备界面红点信息
    /// </summary>
    private void InitEquipUIRedDotInfo()
    {
        AddRedDotInfo(RedDotNames.EQUIP_UI_WEARABLE, "装备界面可穿戴红点");
        AddRedDotInfo(RedDotNames.EQUIP_UI_UPGRADABLE, "装备界面可升级红点");
    }

    /// <summary>
    /// 添加红点信息
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    private bool AddRedDotInfo(string redDotName, string redDotDes)
    {
        if (mRedDotInfoMap.ContainsKey(redDotName))
        {
            Debug.LogError($"重复添加红点名:{redDotName}信息，添加失败!");
            return false;
        }
        var redDotInfo = new RedDotInfo(redDotName, redDotDes);
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        return true;
    }

    /// <summary>
    /// 构建红点前缀树
    /// </summary>
    private void InitRedDotTree()
    {
        RedDotTrie = new Trie();
        foreach (var redDotInfo in mRedDotInfoMap)
        {
            RedDotTrie.AddWord(redDotInfo.Key);
        }
    }

    /// <summary>
    /// 初始化红点运算单元信息
    /// </summary>
    private void InitRedDotUnitInfo()
    {
        mRedDotUnitInfoMap = new Dictionary<RedDotUnit, RedDotUnitInfo>();
        // 构建添加所有游戏里的红点运算单元信息
        InitMainUIRedDotUnitInfo();
        InitBackpackUIRedDotUnitInfo();
        InitMailUIRedDotUnitInfo();
        InitEquipUIRedDotUnitInfo();
    }

    /// <summary>
    /// 初始化主界面红点运算单元信息
    /// </summary>
    private void InitMainUIRedDotUnitInfo()
    {
        RedDotUnitInfo redDotUnitInfo;
        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_FUNC1, "动态新功能1解锁", RedDotUtilities.CaculateNewFunc1, RedDotType.NEW);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_NEW_FUNC1);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_FUNC2, "动态新功能2解锁", RedDotUtilities.CaculateNewFunc2, RedDotType.NEW);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_NEW_FUNC2);
    }

    /// <summary>
    /// 初始化背包界面红点运算单元信息
    /// </summary>
    private void InitBackpackUIRedDotUnitInfo()
    {
        RedDotUnitInfo redDotUnitInfo;
        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_ITEM_NUM, "新道具数", RedDotUtilities.CaculateNewItemNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU_BACKPACK);
        redDotUnitInfo.AddRedDotName(RedDotNames.BACKPACK_UI_ITEM_TAG);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_RESOURCE_NUM, "新资源数", RedDotUtilities.CaculateNewResourceNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU_BACKPACK);
        redDotUnitInfo.AddRedDotName(RedDotNames.BACKPACK_UI_RESOURCE_TAG);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_EQUIP_NUM, "新装备数", RedDotUtilities.CaculateNewEquipNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU_EQUIP);
        redDotUnitInfo.AddRedDotName(RedDotNames.BACKPACK_UI_EQUIP_TAG);
    }

    /// <summary>
    /// 初始化邮件界面红点运算单元信息
    /// </summary>
    private void InitMailUIRedDotUnitInfo()
    {
        RedDotUnitInfo redDotUnitInfo;
        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_PUBLIC_MAIL_NUM, "新公共邮件数", RedDotUtilities.CaculateNewPublicMailNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MAIL);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIL_UI_PUBLIC_MAIL);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_BATTLE_MAIL_NUM, "新战斗邮件数", RedDotUtilities.CaculateNewBattleMailNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MAIL);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIL_UI_BATTLE_MAIL);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.NEW_OTHER_MAIL_NUM, "新其他邮件数", RedDotUtilities.CaculateNewOtherMailNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MAIL);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIL_UI_OTHER_MAIL);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.PUBLIC_MAIL_REWARD_NUM, "公共邮件可领奖数", RedDotUtilities.CaculateNewPublicMailRewardNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MAIL);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIL_UI_PUBLIC_MAIL);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.BATTLE_MAIL_REWARD_NUM, "战斗邮件可领奖数", RedDotUtilities.CaculateNewBattleMailRewardNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MAIL);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIL_UI_BATTLE_MAIL);
    }

    /// <summary>
    /// 初始化装备界面红点运算单元信息
    /// </summary>
    private void InitEquipUIRedDotUnitInfo()
    {
        RedDotUnitInfo redDotUnitInfo;
        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.WEARABLE_EQUIP_NUM, "可穿戴装备数", RedDotUtilities.CaculateWearableEquipNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU_EQUIP);
        redDotUnitInfo.AddRedDotName(RedDotNames.EQUIP_UI_WEARABLE);

        redDotUnitInfo = AddRedDotUnitInfo(RedDotUnit.UPGRADEABLE_EQUIP_NUM, "可升级装备数", RedDotUtilities.CaculateUpgradeableEquipNum, RedDotType.NUMBER);
        redDotUnitInfo.AddRedDotName(RedDotNames.MAIN_UI_MENU_EQUIP);
        redDotUnitInfo.AddRedDotName(RedDotNames.EQUIP_UI_UPGRADABLE);
    }

    /// <summary>
    /// 添加红点运算单元信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="redDotUnitDes"></param>
    /// <param name="caculateFunc"></param>
    /// <param name="redDotType"></param>
    /// <returns></returns>
    private RedDotUnitInfo AddRedDotUnitInfo(RedDotUnit redDotUnit, string redDotUnitDes, Func<int> caculateFunc, RedDotType redDotType = RedDotType.NUMBER)
    {
        RedDotUnitInfo redDotUnitInfo;
        if(mRedDotUnitInfoMap.TryGetValue(redDotUnit, out redDotUnitInfo))
        {
            Debug.LogError($"已添加红点运算单元:{redDotUnit.ToString()}的红点运算单元信息,请勿重复添加,添加失败!");
            return redDotUnitInfo;
        }
        redDotUnitInfo = new RedDotUnitInfo(redDotUnit, redDotUnitDes, caculateFunc, redDotType);
        mRedDotUnitInfoMap.Add(redDotUnit, redDotUnitInfo);
        return redDotUnitInfo;
    }

    /// <summary>
    /// 根据mRedDotUnitNameMap反向构建mRedDotNameUnitMap
    /// </summary>
    private void InitRedDotNameUnitMap()
    {
        if(mRedDotNameUnitMap == null)
        {
            mRedDotNameUnitMap = new Dictionary<string, List<RedDotUnit>>();
        }
        mRedDotNameUnitMap.Clear();
        foreach(var redDotUnitInfo in mRedDotUnitInfoMap)
        {
            foreach(var redDotName in redDotUnitInfo.Value.RedDotNameLsit)
            {
                if(!mRedDotNameUnitMap.ContainsKey(redDotName))
                {
                    mRedDotNameUnitMap.Add(redDotName, new List<RedDotUnit>());
                }
                if(!mRedDotNameUnitMap[redDotName].Contains(redDotUnitInfo.Key))
                {
                    mRedDotNameUnitMap[redDotName].Add(redDotUnitInfo.Key);
                }
            }
        }
    }

    /// <summary>
    /// 获取红点名和红点信息Map<红点名, 红点信息>
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, RedDotInfo> GetRedDotInfoMap()
    {
        return mRedDotInfoMap;
    }

    /// <summary>
    /// 获取红点运算单元信息Map<红点运算单元门, 红点运算单元信息>
    /// </summary>
    /// <returns></returns>
    public Dictionary<RedDotUnit, RedDotUnitInfo> GetRedDotUnitInfoMap()
    {
        return mRedDotUnitInfoMap;
    }

    /// <summary>
    /// 获取红点名和红点运算单元列表Map<红点名, 红点运算单元列表>
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, List<RedDotUnit>> GetRedDotNameUnitMap()
    {
        return mRedDotNameUnitMap;
    }

    /// <summary>
    /// 获取指定红点名的红点信息
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public RedDotInfo GetRedDotInfoByName(string redDotName)
    {
        RedDotInfo redDotInfo;
        if(!mRedDotInfoMap.TryGetValue(redDotName, out redDotInfo))
        {
            Debug.LogError($"找不到红点名:{redDotName}的红点信息!");
        }
        return redDotInfo;
    }

    /// <summary>
    /// 获取指定红点运算单元的红点运算单元信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public RedDotUnitInfo GetRedDotUnitInfo(RedDotUnit redDotUnit)
    {
        RedDotUnitInfo redDotUnitInfo;
        if(!mRedDotUnitInfoMap.TryGetValue(redDotUnit, out redDotUnitInfo))
        {
            Debug.LogError($"找不到红点运算单元:{redDotUnit.ToString()}的信息!");
        }
        return redDotUnitInfo;
    }

    /// <summary>
    /// 获取指定红点运算单元影响的红点名列表
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public List<string> GetRedDotUnitNames(RedDotUnit redDotUnit)
    {
        RedDotUnitInfo redDotUnitInfo = GetRedDotUnitInfo(redDotUnit);
        return redDotUnitInfo?.RedDotNameLsit;
    }

    /// <summary>
    /// 获取指定红点运算单元的计算委托
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public Func<int> GetRedDotUnitFunc(RedDotUnit redDotUnit)
    {
        RedDotUnitInfo redDotUnitInfo = GetRedDotUnitInfo(redDotUnit);
        return redDotUnitInfo?.RedDotUnitCalculateFunc;
    }

    /// <summary>
    /// 获取指定红点运算单元的红点类型
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public RedDotType GetRedDotUnitRedType(RedDotUnit redDotUnit)
    {
        RedDotUnitInfo redDotUnitInfo = GetRedDotUnitInfo(redDotUnit);
        if(redDotUnitInfo != null)
        {
            return redDotUnitInfo.RedDotType;
        }
        Debug.LogError($"获取红点运算单元:{redDotUnit}的红点类型失败!");
        return RedDotType.NONE;
    }

    /// <summary>
    /// 获取指定红点名的所有红点运算单元列表
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public List<RedDotUnit> GetRedDotUnitsByName(string redDotName)
    {
        List<RedDotUnit> redDotUnitList;
        if(!mRedDotNameUnitMap.TryGetValue(redDotName, out redDotUnitList))
        {
            Debug.LogError($"找不到红点名:{redDotName}的红点运算单元礼包!");
        }
        return redDotUnitList;
    }
}