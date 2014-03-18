using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Wz.Common
{
    public static class CommonType
    {
        #region<<枚举>>

        public enum FactoryType
        {
            /// <summary>
            /// 淘宝
            /// </summary>
            Top,
            /// <summary>
            /// 京东
            /// </summary>
            Jos,
            Null
        }

        public enum EDownProductType
        {
            /// <summary>
            /// 出售中
            /// </summary>
            OnSale,
            /// <summary>
            /// 仓库中
            /// </summary>
            InStock,
            /// <summary>
            /// 全部
            /// </summary>
            All
        }

        /// <summary>
        /// 短信模板类型
        /// </summary>
        public enum ESMSModelType
        {
            /// <summary>
            /// 生日关怀
            /// </summary>
            [Description("生日关怀")]
            BirthDay,
            /// <summary>
            /// 等级升级提醒
            /// </summary>
            [Description("升级提醒")]
            LevelUp,
            /// <summary>
            /// 购买提醒
            /// </summary>
            [Description("购买提醒")]
            PurchaseInfo,
            /// <summary>
            /// 其它
            /// </summary>
            [Description("其它")]
            Other
        }

        /// <summary>
        /// 会员扩展计算属性
        /// </summary>
        public enum EMemberExtensions
        {
            /// <summary>
            /// 生日
            /// </summary>
            [Description("生日")]
            BirthDay,
            /// <summary>
            /// 年龄
            /// </summary>
            [Description("年龄")]
            Age,
            /// <summary>
            /// 身高
            /// </summary>
            [Description("身高")]
            High,
            /// <summary>
            /// 体重
            /// </summary>
            [Description("体重")]
            Weight
        }

        /// <summary>
        /// 短信平台参数类型
        /// </summary>
        public enum ESMSParamType
        {
            /// <summary>
            /// 固定参数
            /// </summary>
            [Description("指定参数")]
            CONST,
            /// <summary>
            /// 输入参数
            /// </summary>
            [Description("输入参数")]
            INPUT
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public enum EDataStatus
        {
            /// <summary>
            /// 未定义
            /// </summary>
            [Description("未定义")]
            DATA_STATUS_UNDEFINED,
            /// <summary>
            /// 未定义
            /// </summary>
            [Description("有效")]
            DATA_STATUS_VALID,
            /// <summary>
            /// 未定义
            /// </summary>
            [Description("无效")]
            DATA_STATUS_INVALID
        }

        /// <summary>
        /// 图片类型
        /// </summary>
        public enum EImageType
        {
            /// <summary>
            /// 厚度指数
            /// </summary>
            [Description("厚度指数")]
            Thickness,
            /// <summary>
            /// 手感指数
            /// </summary>
            [Description("手感指数")]
            Touch,
            /// <summary>
            /// 版型指数
            /// </summary>
            [Description("版型指数")]
            Type,
            /// <summary>
            /// 弹性指数
            /// </summary>
            [Description("弹性指数")]
            Spring,
            /// <summary>
            /// 保暖指数
            /// </summary>
            [Description("保暖指数")]
            Warm,
            /// <summary>
            /// 吸湿指数
            /// </summary>
            [Description("吸湿指数")]
            Absorption,
            /// <summary>
            /// 透气指数
            /// </summary>
            [Description("透气指数")]
            Breathable,
            /// <summary>
            /// 面料介绍
            /// </summary>
            [Description("面料介绍")]
            Fabric,
            /// <summary>
            /// 洗涤说明
            /// </summary>
            [Description("洗涤说明")]
            Washing,
            /// <summary>
            /// 尺寸表
            /// </summary>
            [Description("尺寸表")]
            Size,
            /// <summary>
            /// 模特信息
            /// </summary>
            [Description("模特信息")]
            ModelInfo,
            /// <summary>
            /// 品牌介绍
            /// </summary>
            [Description("品牌介绍")]
            Brand,
            /// <summary>
            /// 质量保证
            /// </summary>
            [Description("质量保证")]
            Quality,
            /// <summary>
            /// 授权书
            /// </summary>
            [Description("授权书")]
            Authorization,
            /// <summary>
            /// 购物保障
            /// </summary>
            [Description("购物保障")]
            Guarantee,
            /// <summary>
            /// 主图
            /// </summary>
            [Description("主图")]
            Main,
            /// <summary>
            /// 商品颜色图
            /// </summary>
            [Description("商品颜色图")]
            Color,
            /// <summary>
            /// 形象展示
            /// </summary>
            [Description("形象展示")]
            M1,
            /// <summary>
            /// 模特主色
            /// </summary>
            [Description("模特主色")]
            M2,
            /// <summary>
            /// 颜色图
            /// </summary>
            [Description("颜色图")]
            MC,
            /// <summary>
            /// 细节图
            /// </summary>
            [Description("细节图")]
            D,
            /// <summary>
            /// 模特半身图
            /// </summary>
            [Description("模特半身图")]
            MH,
            /// <summary>
            /// 模特全身图
            /// </summary>
            [Description("模特全身图")]
            MA
        }

        /// <summary>
        /// 模块种类
        /// </summary>
        public enum EModuleCategory
        {
            /// <summary>
            /// 产品参数
            /// </summary>
            [Description("产品参数")]
            Parameters,
            /// <summary>
            /// 产品参数2
            /// </summary>
            [Description("产品参数2")]
            Parameters2,
            /// <summary>
            /// 模特展示
            /// </summary>
            [Description("模特展示")]
            ModelShow,
            /// <summary>
            /// 细节展示
            /// </summary>
            [Description("细节展示")]
            Details,
            /// <summary>
            /// 面料介绍
            /// </summary>
            [Description("面料介绍")]
            Fabric,
            /// <summary>
            /// 洗涤说明
            /// </summary>
            [Description("洗涤说明")]
            Washing,
            /// <summary>
            /// 尺寸表
            /// </summary>
            [Description("尺寸表")]
            Size,
            /// <summary>
            /// 模特信息
            /// </summary>
            [Description("模特信息")]
            ModelInfo,
            /// <summary>
            /// 品牌介绍
            /// </summary>
            [Description("品牌介绍")]
            Brand,
            /// <summary>
            /// 质量保证
            /// </summary>
            [Description("质量保证")]
            Quality,
            /// <summary>
            /// 授权书
            /// </summary>
            [Description("授权书")]
            Authorization,
            /// <summary>
            /// 购物保障
            /// </summary>
            [Description("购物保障")]
            Guarantee
        }

        /// <summary>
        /// 模板类型
        /// </summary>
        public enum ETemplateType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("普通")]
            NORMAL,
            /// <summary>
            /// Bywho
            /// </summary>
            [Description("UNDERWEAR")]
            UNDERWEAR
        }

        /// <summary>
        /// 淘宝状态-ECRM状态
        /// </summary>
        public enum tbOrderStatus
        {
            /// <summary>
            /// 没有创建支付宝交易-等待买家付款
            /// </summary>
            [Description("ORDER_STATUS_PAY_WAIT")]
            TRADE_NO_CREATE_PAY,
            /// <summary>
            /// 等待买家付款-等待买家付款
            /// </summary>
            [Description("ORDER_STATUS_PAY_WAIT")]
            WAIT_BUYER_PAY,
            /// <summary>
            /// 等待卖家发货,即:买家已付款-等待发货
            /// </summary>
            [Description("ORDER_STATUS_PAY_OVER")]
            WAIT_SELLER_SEND_GOODS,
            /// <summary>
            /// 等待买家收货，即：卖家已发货-发货完成
            /// </summary>
            [Description("ORDER_STATUS_DELIVERY_OVER")]
            WAIT_BUYER_CONFIRM_GOODS,
            /// <summary>
            /// 买家已签收（货到付款）-发货完成
            /// </summary>
            [Description("ORDER_STATUS_DEAL_OVER")]
            TRADE_BUYER_SIGNED,
            /// <summary>
            /// 交易成功-交易完成
            /// </summary>
            [Description("ORDER_STATUS_DEAL_OVER")]
            TRADE_FINISHED,
            /// <summary>
            /// 退款成功交易关闭-交易关闭
            /// </summary>
            [Description("ORDER_STATUS_DEAL_CLOSE")]
            TRADE_CLOSED,
            /// <summary>
            /// 付款前交易关闭-交易关闭
            /// </summary>
            [Description("ORDER_STATUS_DEAL_CLOSE")]
            TRADE_CLOSED_BY_TAOBAO
        }
        public enum sysStatus
        {
            /// <summary>
            /// 允许下载
            /// </summary>
            DOWN,
            /// <summary>
            /// 等待
            /// </summary>
            WAIT
        }


        public enum NOTICE_TYPE
        {
            短信通知,
            邮件通知
        }
        public enum NOTICE_OBJECT
        {
            一笔交易,
            两笔交易,
            三笔交易以上,
            一周之内有交易,
            两周之内有交易,
            一周之内无交易,
            两周之内无交易
        }
        public enum ABNORMAL_ORDERS
        {
            [Description("地址不全")]
            ADDRESS_INCOMPLETE,
            [Description("金额异常")]
            MONEY_ABNORMAL,
            [Description("海外订单")]
            OVERSEAS_ORDERS,
            [Description("缺少SKU")]
            LACK_SKU,
            [Description("非法收货人姓名")]
            ILLEGAL_RECEIVER,
            [Description("其他异常订单")]
            ABNORMAL_ORDERS_OTHER,
            [Description("可疑收货人")]
            SUSPICIOUS_CONSIGNEES,
            [Description("SKU售价异常")]
            SKU_PRICE_ABNORMAL,
            [Description("问题用户")]
            PROBLEM_USER_SKU_ALLERGY
        }

        public enum PRODUCT_SALETYPE
        {
            [Description("一口价")]
            FIXED,
            [Description("拍卖")]
            AUCTION
        }

        public enum FREIGHT_PAYER
        {
            [Description("卖家承担")]
            seller = 1,
            [Description("买家承担")]
            buyer = 2
        }
        /// <summary>
        /// 新旧程度
        /// </summary>
        public enum STUFF_STATUS
        {
            [Description("全新")]
            NEW = 1,
            [Description("二手")]
            SECOND = 2,
            [Description("闲置")]
            UNUSERD = 3,
            [Description("未指定")]
            NONE = 4
        }

        /// <summary>
        /// 商品上传后的状态
        /// </summary>
        public enum APPROVE_STATUS
        {
            [Description("出售中")]
            onsale = 0,
            [Description("仓库中")]
            instock = 1
        }

        /// <summary>
        /// 模板替换内容
        /// </summary>
        public enum INPUT_TYPE
        {
            /// <summary>
            /// 文本1
            /// </summary>
            [Description("文本1")]
            INPUT_TYPE_TEXT1,
            /// <summary>
            /// 文本1
            /// </summary>
            [Description("文本2")]
            INPUT_TYPE_TEXT2,
            /// <summary>
            /// 图片
            /// </summary>
            [Description("图片")]
            INPUT_TYPE_IMAGE,
            /// <summary>
            /// 图片链接
            /// </summary>
            [Description("图片链接")]
            INPUT_TYPE_LINK
        }

        /// <summary>
        /// 循环类型
        /// </summary>
        public enum REPEAT_TYPE
        {
            /// <summary>
            /// 未指定
            /// </summary>
            [Description("未指定")]
            REPEAT_TYPE_NONE,
            /// <summary>
            /// 交叉样式
            /// </summary>
            [Description("交叉样式")]
            REPEAT_TYPE_ALTER,
            /// <summary>
            /// 普通循环
            /// </summary>
            [Description("普通循环")]
            REPEAT_TYPE_STATIC
        }

        /// <summary>
        /// 模块类型
        /// </summary>
        public enum MODULE_TYPE
        {
            /// <summary>
            /// 未指定
            /// </summary>
            [Description("未指定")]
            MODULE_TYPE_NONE,
            /// <summary>
            /// 通用模块
            /// </summary>
            [Description("通用模块")]
            MODULE_TYPE_GLOBAL,
            /// <summary>
            /// 定制模块
            /// </summary>
            [Description("定制模块")]
            MODULE_TYPE_SPEC
        }

        /// <summary>
        /// 通用模块区分
        /// </summary>
        public enum GLOBAL_MODULE
        {
            /// <summary>
            /// 其它
            /// </summary>
            [Description("其它")]
            GLOBAL_MODULE_OTHER,
            /// <summary>
            /// 促销类型
            /// </summary>
            [Description("促销BANNER")]
            GLOBAL_MODULE_BANNER,

            /// <summary>
            /// 洗涤方式
            /// </summary>
            [Description("洗涤方式")]
            GLOBAL_MODULE_WASHING,

            /// <summary>
            /// 包装说明
            /// </summary>
            [Description("包装说明")]
            GLOBAL_MODULE_PACKAGE
        }

        /// <summary>
        /// 仓库异动类型
        /// </summary>
        public enum STOCK_CHANGE_TYPE
        {

            /// <summary>
            /// 未指定
            /// </summary>
            [Description("未指定")]
            STOCK_CHANGE_NONE,
            /// <summary>
            /// 采购入库
            /// </summary>
            [Description("采购入库")]
            STOCK_CHANGE_IN_PURCHASE,
            /// <summary>
            /// 商品出库
            /// </summary>
            [Description("商品出库")]
            STOCK_CHANGE_OUT,
            /// <summary>
            /// 退货入库
            /// </summary>
            [Description("退货入库")]
            STOCK_CHANGE_IN_RETURN,
            /// <summary>
            /// 盘盈入库
            /// </summary>
            [Description("盘盈入库")]
            STOCK_CHANGE_IN_PY,
            /// <summary>
            /// 盘亏出库
            /// </summary>
            [Description("盘亏出库")]
            STOCK_CHANGE_OUT_PK,
            /// <summary>
            /// 库存校验
            /// </summary>
            [Description("库存校验")]
            STOCK_CHANGE_CORRRECTION,
            /// <summary>
            /// 仓库调仓
            /// </summary>
            [Description("仓库调仓")]
            STOCK_CHANGE_SHIFT,
            /// <summary>
            /// 换货入库
            /// </summary>
            [Description("换货入库")]
            STOCK_CHANGE_EXCHANGE_IN,
            /// <summary>
            /// 换货出库
            /// </summary>
            [Description("换货出库")]
            STOCK_CHANGE_EXCHANGE_OUT,
            /// <summary>
            /// 订单变更入库
            /// </summary>
            [Description("订单变更入库")]
            STOCK_CHANGE_ORDER_CHANGE_IN,
            /// <summary>
            /// 订单变更出库
            /// </summary>
            [Description("订单变更出库")]
            STOCK_CHANGE_ORDER_CHANGE_OUT,
            /// <summary>
            /// 创建预售
            /// </summary>
            [Description("创建预售")]
            STOCK_CHANGE_BOOK_CREATE,
            /// <summary>
            /// 预售清零
            /// </summary>
            [Description("预售清零")]
            STOCK_CHANGE_BOOK_CANCEL,
            /// <summary>
            /// 预售入库
            /// </summary>
            [Description("预售入库")]
            STOCK_CHANGE_BOOK_IN,
            /// <summary>
            /// 预售出库
            /// </summary>
            [Description("预售出库")]
            STOCK_CHANGE_BOOK_OUT
        }

        /// <summary>
        /// 来源
        /// </summary>
        public enum AD_FROM
        {
            /// <summary>
            /// 官网
            /// </summary>
            [Description("官网")]
            OFFICIAL,
            /// <summary>
            /// 电话
            /// </summary>
            [Description("电话")]
            PHONE,
            /// <summary>
            /// 淘宝B
            /// </summary>
            [Description("淘宝B")]
            TAOBAO_B,
            /// <summary>
            /// 淘宝C
            /// </summary>
            [Description("淘宝C")]
            TAOBAO_C
        }

        /// <summary>
        /// 发货单状态
        /// </summary>
        public enum DELIVERY_STATUS
        {
            /// <summary>
            /// 未推送
            /// </summary>
            [Description("未推送")]
            DELIVERY_STATUS_PUSH_WAIT,
            /// <summary>
            /// 已推送
            /// </summary>
            [Description("已推送")]
            DELIVERY_STATUS_PUSH_OVER,
            /// <summary>
            /// 已发货
            /// </summary>
            [Description("已发货")]
            DELIVERY_STATUS_SEND_OVER,
            /// <summary>
            /// 已取消
            /// </summary>
            [Description("已取消")]
            DELIVERY_STATUS_SEND_CANCEL
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum ORDER_STATUS
        {
            /// <summary>
            /// 等待买家付款
            /// </summary>
            [Description("等待买家付款")]
            ORDER_STATUS_PAY_WAIT,
            /// <summary>
            /// 买家已付款
            /// </summary>
            [Description("买家已付款")]
            ORDER_STATUS_PAY_OVER,
            /// <summary>
            /// 等待发货
            /// </summary>
            [Description("等待发货")]
            ORDER_STATUS_DELIVERY_WAIT,
            /// <summary>
            /// 等待发货
            /// </summary>
            [Description("已生成发货单")]
            ORDER_STATUS_DELIVERY_CREATE,
            /// <summary>
            /// 部分发货
            /// </summary>
            [Description("部分发货")]
            ORDER_STATUS_DELIVERY_PART,
            /// <summary>
            /// 完成发货
            /// </summary>
            [Description("完成发货")]
            ORDER_STATUS_DELIVERY_OVER,
            /// <summary>
            /// 交易完成
            /// </summary>
            [Description("交易完成")]
            ORDER_STATUS_DEAL_OVER,
            /// <summary>
            /// 交易关闭
            /// </summary>
            [Description("交易关闭")]
            ORDER_STATUS_DEAL_CLOSE
        }

        public enum PayType
        {
            /// <summary>
            /// 支付宝
            /// </summary>
            [Description("支付宝")]
            AlIPAY,
            /// <summary>
            /// 支付宝
            /// </summary>
            [Description("支付宝")]
            ALIPAY,
            /// <summary>
            /// 银行付款
            /// </summary>
            [Description("银行付款")]
            BANK_TRANSFER,
            /// <summary>
            /// 货到付款
            /// </summary>
            [Description("货到付款")]
            CASH_ON_DELIVERY,
            /// <summary>
            /// 信用卡
            /// </summary>
            [Description("信用卡")]
            CREDIT_CARD,
            /// <summary>
            /// 预存款
            /// </summary>
            [Description("预存款")]
            REFUND
        }

        public enum ORDER_ACTION
        {
            已下载订单,
            待审核订单,
            待处理问题订单,
            已修改订单,
            已合并订单,
            缺货订单,
            已拆分订单,
            生成发货单,
            已推送发货单,
            待处理退款,
            未审核退货,
            未审核换货,
            已出库发货单,
            交易完成

        }


        /// <summary>
        /// OB电话拨打状态
        /// </summary>
        public enum TELSTATE
        {
            /// <summary>
            /// 未拨打 = 0
            /// </summary>
            未拨打 = 0,
            /// <summary>
            /// 未联络上_关机 = 1
            /// </summary>
            未联络上_关机 = 1,
            /// <summary>
            /// 未联络上_空号 = 2
            /// </summary>
            未联络上_空号 = 2,
            /// <summary>
            /// 未联络上_音忙 = 3
            /// </summary>
            未联络上_音忙 = 3,
            /// <summary>
            /// 未联络上_不在服务区 = 4
            /// </summary>
            未联络上_不在服务区 = 4,
            /// <summary>
            /// 未联络上_停机 = 5
            /// </summary>
            未联络上_停机 = 5,
            /// <summary>
            /// 成功 = 6
            /// </summary>
            成功 = 6,
            /// <summary>
            /// 考虑 = 7
            /// </summary>
            考虑 = 7,
            /// <summary>
            /// 拒绝_不需要 = 8
            /// </summary>
            拒绝_不需要 = 8,
            /// <summary>
            ///  拒绝_没兴趣 = 9
            /// </summary>
            拒绝_没兴趣 = 9,
            /// <summary>
            /// 拒绝_拒绝电话营销 = 10
            /// </summary>
            拒绝_拒绝电话营销 = 10,
            /// <summary>
            /// 拒绝_其他 = 11
            /// </summary>
            拒绝_其他 = 11
        }

        public static string GetEnumName<T>(this T sender)
        {
            return Enum.GetName(typeof(T), sender);
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static string ToDescription<T>(this T sender)
        {
            Type type = typeof(T);
            FieldInfo info = type.GetField(sender.ToString());
            DescriptionAttribute descriptionAttribute = info.GetCustomAttributes(typeof(DescriptionAttribute), true)[0] as DescriptionAttribute;

            if (descriptionAttribute != null)
                return descriptionAttribute.Description;
            else
                return type.ToString();
        }

        /// <summary>
        /// 对应的字符转换为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="valueName">名称</param>
        /// <returns>T</returns>
        public static T FromNameToEnum<T>(this string valueName)
        {
            Type type = typeof(T);
            FieldInfo info = type.GetField(valueName);
            if (info == null) return default(T);

            T value = (T)info.GetRawConstantValue();

            return value;
        }
        #endregion

        #region <<促销活动>>

        /// <summary>
        /// 通知方式
        /// </summary>
        public enum ESendMethod
        {
            /// <summary>
            /// 短信
            /// </summary>
            [Description("短信")]
            SMS,
            /// <summary>
            /// 电子邮件
            /// </summary>
            [Description("电子邮件")]
            EDM,
            /// <summary>
            /// OB
            /// </summary>
            [Description("OB")]
            OB,
            /// <summary>
            /// 即时通讯
            /// </summary>
            [Description("即时通讯")]
            IM
        }

        /// <summary>
        /// 活动类型
        /// </summary>
        public enum EPromotionType
        {
            /// <summary>
            /// 满额优惠
            /// </summary>
            [Description("满额优惠")]
            FULFIL_QUOTA,
            /// <summary>
            /// 商品优惠
            /// </summary>
            [Description("商品优惠")]
            FULFIL_PRODUCTS,
            /// <summary>
            /// 定额优惠
            /// </summary>
            [Description("定额优惠")]
            QUOTA_DISCOUNT,
            /// <summary>
            /// VIP返利
            /// </summary>
            [Description("VIP返利")]
            VIP_DISCOUNT
        }

        /// <summary>
        /// 订单活动应用状态
        /// </summary>
        public enum EActivityApplyStatus
        {
            /// <summary>
            /// 待处理
            /// </summary>
            [Description("待处理")]
            APPLY_WAIT,
            /// <summary>
            /// 处理中
            /// </summary>
            [Description("处理中")]
            APPLY_DOING,
            /// <summary>
            /// 处理完成
            /// </summary>
            [Description("处理完成")]
            APPLY_OVER,
            /// <summary>
            /// 取消中
            /// </summary>
            [Description("取消中")]
            APPLY_CANCELING,
            /// <summary>
            /// 取消完成
            /// </summary>
            [Description("取消完成")]
            APPLY_CANCELED
        }

        /// <summary>
        /// 活动应用步骤
        /// </summary>
        public enum EStepStatus
        {
            /// <summary>
            /// 待处理
            /// </summary>
            [Description("待处理")]
            STEP_WAIT,
            /// <summary>
            /// 处理完成
            /// </summary>
            [Description("处理完成")]
            STEP_OVER,
            /// <summary>
            /// 取消
            /// </summary>
            [Description("取消")]
            STEP_CANCEL
        }

        #region <<数据类型>>

        /// <summary>
        /// 促销活动状态
        /// </summary>
        public enum SALE_PROMOTION_STATUS
        {
            /// <summary>
            /// 未审核
            /// </summary>
            [Description("未审核")]
            SALE_PROMOTION_UNAUDITED,
            /// <summary>
            /// 已审核,等待发布
            /// </summary>
            [Description("已审核,等待发布")]
            SALE_PROMOTION_AUDITED,
            /// <summary>
            /// 已发布
            /// </summary>
            [Description("已发布")]
            SALE_PROMOTION_RELEASE,
            /// <summary>
            /// 已过期
            /// </summary>
            [Description("已过期")]
            SALE_PROMOTION_EXPIRED
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType
        {
            INT,
            DECIMAL,
            STRING,
            DATETIME
        }

        /// <summary>
        /// 关系运算符
        /// </summary>
        public enum OPERATOR_RELATION
        {
            /// <summary>
            /// 等于
            /// </summary>
            [Description("等于")]
            OP_E,
            /// <summary>
            /// 不等于
            /// </summary>
            [Description("不等于")]
            OP_NE,
            /// <summary>
            /// 大于
            /// </summary>
            [Description("大于")]
            OP_GT,
            /// <summary>
            /// 大于等于
            /// </summary>
            [Description("大于等于")]
            OP_NLT,
            /// <summary>
            /// 小于
            /// </summary>
            [Description("小于")]
            OP_LT,
            /// <summary>
            /// 小于等于
            /// </summary>
            [Description("小于等于")]
            OP_NGT,
            /// <summary>
            /// 包含
            /// </summary>
            [Description("包含")]
            OP_CONTANT,
            /// <summary>
            /// BETWEEN
            /// </summary>
            [Description("BETWEEN")]
            OP_BETWEEN
        }

        /// <summary>
        /// 逻辑运算符
        /// </summary>
        public enum OPERATOR_LOGIC
        {
            /// <summary>
            /// 并且
            /// </summary>
            [Description("并且")]
            AND,
            /// <summary>
            /// 或者
            /// </summary>
            [Description("或者")]
            OR
        }

        #endregion

        #region <<优惠内容>>
        public enum Preferential
        {
            打折 = 0,
            立减 = 1,
            换购 = 2,
            送赠品 = 3,
            包邮 = 4
        }

        /// <summary>
        /// 优惠结果
        /// </summary>
        public enum EPreferential
        {
            /// <summary>
            /// 打折
            /// </summary>
            [Description("打折")]
            DISCOUNT,
            /// <summary>
            /// 第二件打折
            /// </summary>
            [Description("第二件打折")]
            DISCOUNT_SECOND,
            /// <summary>
            /// 立减
            /// </summary>
            [Description("立减")]
            PRICEDOWN,
            /// <summary>
            /// 送赠品
            /// </summary>
            [Description("送赠品")]
            GIFT_GIVE,
            /// <summary>
            /// 送包邮卡
            /// </summary>
            [Description("送包邮卡")]
            POSTAGECARD_GIVE,
            /// <summary>
            /// 送积分
            /// </summary>
            [Description("送积分")]
            POINT_GIVE,
            /// <summary>
            /// 多倍积分
            /// </summary>
            [Description("多倍积分")]
            MUTIPOINT_GIVE,
            /// <summary>
            /// 送优惠券
            /// </summary>
            [Description("送优惠券")]
            COUPON_GIVE,
            /// <summary>
            /// 定额任选
            /// </summary>
            [Description("定额任选")]
            QUOTA_OPTION,
            /// <summary>
            /// 免邮
            /// </summary>
            [Description("免邮")]
            POSTAGE_FREE,
            /// <summary>
            /// 加钱换物
            /// </summary>
            [Description("加钱换物")]
            EXTRA_BARTER
        }
        #endregion



        #region 退换货API 官方调用类型
        public enum ReturnStautsForAPI
        {
            /// <summary>
            /// 新建退货单 100申请退货
            /// </summary>
            CREATE_RETURN,

            /// <summary>
            /// 退货符合要求/等待退款 110已退货等待退款
            /// </summary>
            RETURN_WMSOK,

            /// <summary>
            /// 退货符合要求/退款完成 120已退货已退款
            /// </summary>
            RETURN_CASH,

            /// <summary>
            /// 取消退货    190取消退货
            /// </summary>
            RETURN_CANCEL,

            /// <summary>
            /// 新建换货单 200申请换货
            /// </summary>
            CREATE_EXCHANGE,

            /// <summary>
            /// 退货符合要求/换货发货指示 210已退货/换货发货安排中
            /// </summary>
            EXCHANGE_WMSOK,

            /// <summary>
            /// 换货发货    220已退货/换货发货安排中
            /// </summary>
            EXCHANGE_SEND,

            /// <summary>
            /// 换货送达  230已退货/换货发货完成
            /// </summary>
            EXCHANGE_OVER,

            /// <summary>
            /// 取消换货  290取消
            /// </summary>
            EXCHANGE_CANCEL,

            /// <summary>
            /// 重新发货  300发货安排中
            /// </summary>
            SAND_AGAIN,

            /// <summary>
            /// 重新发货完毕  320发货完成
            /// </summary>
            SEND_OVER,

            /// <summary>
            /// 取消发货 390取消
            /// </summary>
            SEND_CANCEL
        }
        #endregion


        #region 官方订单状态调用类型
        public enum OfficialOrderStatus
        {
            /// <summary>
            /// 等待买家付款
            /// </summary>
            ORDER_STATUS_PAY_WAIT,

            /// <summary>
            /// 买家已付款
            /// </summary>
            ORDER_STATUS_PAY_OVER,
            /// <summary>
            /// 已生成发货单
            /// </summary>
            ORDER_STATUS_DELIVERY_CREATE,
            /// <summary>
            /// 等待发货
            /// </summary>
            ORDER_STATUS_DELIVERY_WAIT,
            /// <summary>
            /// 部分发货
            /// </summary>
            ORDER_STATUS_DELIVERY_PART,
            /// <summary>
            /// 完成发货
            /// </summary>
            ORDER_STATUS_DELIVERY_OVER,
            /// <summary>
            /// 交易完成
            /// </summary>
            ORDER_STATUS_DEAL_OVER,
            /// <summary>
            /// 交易关闭
            /// </summary>
            ORDER_STATUS_DEAL_CLOSE
            //ORDER_STATUS_DELIVERY_CREATE  已生成发货单
        }

        #endregion

        #region<<模板类型>>
        public enum TemplateType
        {
            /// <summary>
            /// 优惠券发放通知
            /// </summary>
            [Description("优惠券发放通知")]
            TEMPLATE_TYPE_COUPON,
            /// <summary>
            /// 催付通知
            /// </summary>
            [Description("催付通知")]
            TEMPLATE_TYPE_DUNNING,
            /// <summary>
            /// 促销通知
            /// </summary>
            [Description("促销通知")]
            TEMPLATE_TYPE_PROMOTION,
            /// <summary>
            /// 发货通知
            /// </summary>
            [Description("发货通知")]
            TEMPLATE_TYPE_SEND
        }

        #endregion
        #endregion
        static public List<string> GetEnumList(Type enumName)
        {
            List<string> enumcontexts = new List<string>();
            //Type enumNames = typeof(enumName);
            foreach (string s in Enum.GetNames(enumName))
            {
                enumcontexts.Add(s);
            }
            return enumcontexts;
        }
    }
}