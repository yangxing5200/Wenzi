using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

namespace Wz.Common
{
    public class XMLReadHelp
    {
        XmlDocument document = new XmlDocument();

        public object GetNode(string Path, string node)
        {
            return null;
        }

        /// <summary>
        /// 按表结构 、根据表结构
        /// </summary>
        /// <param name="ColumnNames">主要表结构</param>
        /// <param name="NotIn">不是表结构的节点。如果父节点不是表中字段、子节点也不是</param>
        /// <returns></returns>
        public DataTable XMLToDataTable(XmlNode ParentNode, DataTable dt, Dictionary<string, Type> ColumnNames, List<string> NotIn)
        {
            XmlNodeList nodelist = ParentNode.ChildNodes;
            foreach (XmlNode node in nodelist)
            {
                int isIn = NotIn.Where(t => t == node.Name).ToList().Count;
                if (isIn > 0)
                {
                    continue;
                }

                //如果有子节点、则查看其子节点
                if (node.FirstChild != null && node.FirstChild.NodeType == XmlNodeType.Element)
                {
                    foreach (XmlAttribute item in node.Attributes) //如果有子节点。则检查其Attribute
                    {
                        KeyValuePair<string, Type> column = ColumnNames.FirstOrDefault(t => t.Key == item.Name);
                        if (!column.Equals(new KeyValuePair<string, Type>()))
                        {
                            DataColumn dc = new DataColumn(column.Key, column.Value);
                            dt.Columns.Add(dc);
                            if (dt.Rows.Count == 0)
                            {
                                DataRow dr = dt.NewRow();
                                dt.Rows.Add(dr);
                            }
                            switch (column.Value.FullName)
                            {
                                case "System.Int32":
                                    dt.Rows[0][column.Key] = int.Parse(item.InnerText);
                                    break;
                                case "System.DateTime":
                                    dt.Rows[0][column.Key] = DateTime.Parse(item.InnerText);
                                    break;
                                default:
                                    dt.Rows[0][column.Key] = item.InnerText;
                                    break;
                            }
                        }
                    }
                    dt = XMLToDataTable(node, dt, ColumnNames, NotIn);
                }
                else  //没有子节点，根据其节点名称、数据类型创建一个列并，根据其值设一行并赋值
                {
                    KeyValuePair<string, Type> column = ColumnNames.FirstOrDefault(t => t.Key == node.Name);
                    if (!column.Equals(new KeyValuePair<string, Type>()))
                    {
                        DataColumn dc = new DataColumn(column.Key, column.Value);
                        dt.Columns.Add(dc);
                        if (dt.Rows.Count == 0)
                        {
                            DataRow dr = dt.NewRow();
                            dt.Rows.Add(dr);
                        }
                        switch (column.Value.FullName)
                        {
                            case "System.Int32":
                                dt.Rows[0][column.Key] = node.InnerText != null ? int.Parse(node.InnerText) : 0;
                                break;
                            case "System.DateTime":
                                dt.Rows[0][column.Key] = node.InnerText != null ? DateTime.Parse(node.InnerText) : DateTime.MinValue;
                                break;
                            default:
                                dt.Rows[0][column.Key] = node.InnerText;
                                break;
                        }
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取自定义的字段值。一般指的是XML里的字典数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        public Dictionary<string, string> getCustomerData(Dictionary<string, string> list, XmlNode ParentNode, string NodeName)
        {
            XmlNodeList nodelist = ParentNode.ChildNodes;
            foreach (XmlNode item in nodelist)
            {
                //如果有子节点、则查看其子节点
                if (item.FirstChild != null && item.FirstChild.NodeType == XmlNodeType.Element)
                {
                    getCustomerData(list, item, NodeName);
                }
                else  //没有子节点， 
                {
                    if (ParentNode.Name == NodeName)
                    {
                        list.Add(item.Name, item.InnerText);
                    }
                }
            }
            return list;
        }
    }
}
