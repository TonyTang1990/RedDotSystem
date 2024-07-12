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
    /// 红点运算单元信息Map<红点运算单元名, 红点运算单元信息>
    /// </summary>
    private Dictionary<RedDotUnit, RedDotUnitInfo> mRedDotUnitInfoMap;

    /// <summary>
    /// 红点运算单元结果值Map<红点运算单元, 红点运算单元结果值>
    /// </summary>
    private Dictionary<RedDotUnit, int> mRedDotUnitResultMap;

    /// <summary>
    /// 红点名和红点信息Map<红点名, 红点信息>
    /// </summary>
    private Dictionary<string, RedDotInfo> mRedDotInfoMap;

    /// <summary>
    /// 红点单元和红点名列表Map
    /// </summary>
    private Dictionary<RedDotUnit, List<string>> mRedDotUnitNameMap;

    /// <summary>
    /// 红点前缀树
    /// </summary>
    public Trie RedDotTrie
    {
        get;
        private set;
    }

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
        mRedDotUnitInfoMap = new Dictionary<RedDotUnit, RedDotUnitInfo>();
        mRedDotUnitResultMap = new Dictionary<RedDotUnit, int>();
        mRedDotInfoMap = new Dictionary<string, RedDotInfo>();
        mRedDotUnitNameMap = new Dictionary<RedDotUnit, List<string>>();
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
        // 优先初始化红点单元，现在改成通过红点名正向配置红点单元组成，而非反向红点单元定义影响红点名组成
        InitRedDotUnitInfo();
        InitRedDotInfo();
        InitRedDotTree();
        // InitRedDotUnitNameMap必须在InitRedDotInfo之后调用，因为用到了前面的数据
        UpdateRedDotUnitNameMap();
        IsInitCompelte = true;
    }

    /// <summary>
    /// 初始化红点运算单元信息
    /// </summary>
    private void InitRedDotUnitInfo()
    {
        // 构建添加所有游戏里的红点运算单元信息
        AddRedDotUnitInfo(RedDotUnit.NEW_FUNC1, "动态新功能1解锁", RedDotUtilities.CaculateNewFunc1, RedDotType.NEW);
        AddRedDotUnitInfo(RedDotUnit.NEW_FUNC2, "动态新功能2解锁", RedDotUtilities.CaculateNewFunc2, RedDotType.NEW);
        AddRedDotUnitInfo(RedDotUnit.NEW_ITEM_NUM, "新道具数", RedDotUtilities.CaculateNewItemNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.NEW_RESOURCE_NUM, "新资源数", RedDotUtilities.CaculateNewResourceNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.NEW_EQUIP_NUM, "新装备数", RedDotUtilities.CaculateNewEquipNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.NEW_PUBLIC_MAIL_NUM, "新公共邮件数", RedDotUtilities.CaculateNewPublicMailNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.NEW_BATTLE_MAIL_NUM, "新战斗邮件数", RedDotUtilities.CaculateNewBattleMailNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.NEW_OTHER_MAIL_NUM, "新其他邮件数", RedDotUtilities.CaculateNewOtherMailNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.PUBLIC_MAIL_REWARD_NUM, "公共邮件可领奖数", RedDotUtilities.CaculateNewPublicMailRewardNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.BATTLE_MAIL_REWARD_NUM, "战斗邮件可领奖数", RedDotUtilities.CaculateNewBattleMailRewardNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.WEARABLE_EQUIP_NUM, "可穿戴装备数", RedDotUtilities.CaculateWearableEquipNum, RedDotType.NUMBER);
        AddRedDotUnitInfo(RedDotUnit.UPGRADEABLE_EQUIP_NUM, "可升级装备数", RedDotUtilities.CaculateUpgradeableEquipNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 初始化红点信息
    /// </summary>
    private void InitRedDotInfo()
    {
        /// Note:
        /// 穷举的好处是足够灵活
        /// 缺点是删除最里层红点运算单元需要把外层所有影响到的红点名相关红点运算单元配置删除
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
        RedDotInfo redDotInfo;
        redDotInfo = AddRedDotInfo(RedDotNames.MAIN_UI_NEW_FUNC1, "主界面新功能1红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_FUNC1);

        redDotInfo = AddRedDotInfo(RedDotNames.MAIN_UI_NEW_FUNC2, "主界面新功能2红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_FUNC2);

        redDotInfo = AddRedDotInfo(RedDotNames.MAIN_UI_MENU, "主界面菜单红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_ITEM_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_RESOURCE_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_EQUIP_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.MAIN_UI_MAIL, "主界面邮件红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_PUBLIC_MAIL_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_BATTLE_MAIL_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_OTHER_MAIL_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.PUBLIC_MAIL_REWARD_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.MAIN_UI_MENU_EQUIP, "主界面菜单装备红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.WEARABLE_EQUIP_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.UPGRADEABLE_EQUIP_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.MAIN_UI_MENU_BACKPACK, "主界面菜单背包红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_ITEM_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_RESOURCE_NUM);
    }

    /// <summary>
    /// 初始化背包界面红点信息
    /// </summary>
    private void InitBackpackUIRedDotInfo()
    {
        RedDotInfo redDotInfo;
        redDotInfo = AddRedDotInfo(RedDotNames.BACKPACK_UI_ITEM_TAG, "背包界面道具页签红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_ITEM_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.BACKPACK_UI_RESOURCE_TAG, "背包界面资源页签红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_RESOURCE_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.BACKPACK_UI_EQUIP_TAG, "背包界面装备页签红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_EQUIP_NUM);
    }

    /// <summary>
    /// 初始化邮件界面红点信息
    /// </summary>
    private void InitMailUIRedDotInfo()
    {
        RedDotInfo redDotInfo;
        redDotInfo = AddRedDotInfo(RedDotNames.MAIL_UI_PUBLIC_MAIL, "邮件界面公共邮件红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_PUBLIC_MAIL_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.PUBLIC_MAIL_REWARD_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.MAIL_UI_BATTLE_MAIL, "邮件界面战斗邮件红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_BATTLE_MAIL_NUM);
        redDotInfo.AddRedDotUnit(RedDotUnit.BATTLE_MAIL_REWARD_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.MAIL_UI_OTHER_MAIL, "邮件界面其他邮件红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.NEW_OTHER_MAIL_NUM);
    }

    /// <summary>
    /// 初始化装备界面红点信息
    /// </summary>
    private void InitEquipUIRedDotInfo()
    {
        RedDotInfo redDotInfo;
        redDotInfo = AddRedDotInfo(RedDotNames.EQUIP_UI_WEARABLE, "装备界面可穿戴红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.WEARABLE_EQUIP_NUM);

        redDotInfo = AddRedDotInfo(RedDotNames.EQUIP_UI_UPGRADABLE, "装备界面可升级红点");
        redDotInfo.AddRedDotUnit(RedDotUnit.UPGRADEABLE_EQUIP_NUM);
    }

    /// <summary>
    /// 添加红点信息
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    private RedDotInfo AddRedDotInfo(string redDotName, string redDotDes)
    {
        if (mRedDotInfoMap.ContainsKey(redDotName))
        {
            Debug.LogError($"重复添加红点名:{redDotName}信息，添加失败!");
            return null;
        }
        var redDotInfo = new RedDotInfo(redDotName, redDotDes);
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        return redDotInfo;
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
    /// 根据mRedDotInfoMap反向构建mRedDotNameUnitMap
    /// </summary>
    private void UpdateRedDotUnitNameMap()
    {
        mRedDotUnitNameMap.Clear();
        foreach (var redDotInfo in mRedDotInfoMap)
        {
            foreach (var redDotUnit in redDotInfo.Value.RedDotUnitList)
            {
                if (!mRedDotUnitNameMap.ContainsKey(redDotUnit))
                {
                    mRedDotUnitNameMap.Add(redDotUnit, new List<string>());
                }
                if (!mRedDotUnitNameMap[redDotUnit].Contains(redDotInfo.Key))
                {
                    mRedDotUnitNameMap[redDotUnit].Add(redDotInfo.Value.RedDotName);
                }
            }
        }
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
        var redDotInfo = GetRedDotInfoByName(redDotName);
        return redDotInfo != null ? redDotInfo.RedDotUnitList : null;
    }

    /// <summary>
    /// 获取指定红点运算单元影响的所有红点名列表
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public List<string> GetRedDotNamesByUnit(RedDotUnit redDotUnit)
    {
        List<string> redDotNames;
        if(!mRedDotUnitNameMap.TryGetValue(redDotUnit, out redDotNames))
        {
            Debug.LogError($"找不到红点运算单元:{redDotUnit.ToString()}的影响红点名信息，获取失败！");
        }
        return redDotNames;
    }

    /// <summary>
    /// 获取指定红点运算单元的运算结果
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public int GetRedDotUnitResult(RedDotUnit redDotUnit)
    {
        int result = 0;
        if(!mRedDotUnitResultMap.TryGetValue(redDotUnit, out result))
        {
        }
        return result;
    }

    /// <summary>
    /// 设置指定红点运算单元的运算结果
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool SetRedDotUnitResult(RedDotUnit redDotUnit, int result)
    {
        if(!mRedDotUnitResultMap.ContainsKey(redDotUnit))
        {
            mRedDotUnitResultMap.Add(redDotUnit, result);
            return true;
        }
        mRedDotUnitResultMap[redDotUnit] = result;
        return true;
    }
}