using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ShapingAPI.Infrastructure.Core
{
    public class Utils
    {
        public static void FilterProperties(JToken token, List<string> fields)
        {
            try
            {
                JContainer container = token as JContainer;
                if (container == null) return;

                JProperty jprop = null;
                JObject jobj = null;

                JProperty pNested = null;
                JObject poNested = null;

                List<JToken> removeList = new List<JToken>();
                foreach (JToken el in container.Children())
                {
                    if (el is JProperty)
                    {
                        jprop = el as JProperty;

                        if (fields.Any(f => f.StartsWith(el.Path.ToLower() + "(")))
                        {
                            string nestedProperty = fields.First(f => f.StartsWith(el.Path.ToLower() + "("));
                            int startField = nestedProperty.IndexOf("(");
                            int lastField = nestedProperty.LastIndexOf(")");
                            string nestedFields = nestedProperty.Substring(startField + 1, (lastField - 1) - startField);

                            List<string> _nestedFieldList = GetNestedFiels(nestedFields);// nestedFields.Split(';').ToList();

                            JToken nestedProperties = el.First();
                            List<JToken> removeListNested = new List<JToken>();
                            foreach (JToken elNested in (nestedProperties as JContainer).Children())
                            {
                                if (elNested is JProperty)
                                {
                                    pNested = elNested as JProperty;
                                    if (!_nestedFieldList.Contains(pNested.Path.ToLower().Substring(pNested.Path.IndexOf('.') + 1)))
                                        removeListNested.Add(pNested);
                                }
                                else if (elNested is JObject)
                                {
                                    poNested = elNested as JObject;

                                    foreach (JToken _poNested in (poNested as JContainer).Children().OrderBy(order => order.Parent))
                                    {
                                        if (!(_poNested.ToString().Contains('(') && _poNested.ToString().Contains(';') && _poNested.ToString().Contains(')'))
                                            && !_poNested.ToString().Replace(" ", "").Replace("\r\n", "").Contains("[{"))
                                        {
                                            if (!_nestedFieldList.Contains(_poNested.Path.ToLower().Substring(_poNested.Path.IndexOf('.') + 1)))
                                                removeListNested.Add(_poNested);
                                        }
                                        else
                                        {
                                            foreach (JToken el2 in (_poNested as JContainer).Children())
                                            {
                                                if (el2 is JProperty)
                                                {
                                                    jprop = el2 as JProperty;

                                                    if (fields.Any(f => f.StartsWith(el2.Path.ToLower() + "(")))
                                                    {
                                                        string nestedProperty2 = fields.First(f => f.StartsWith(el2.Path.ToLower() + "("));
                                                        int startField2 = nestedProperty.IndexOf("(");
                                                        int lastField2 = nestedProperty.LastIndexOf(")");
                                                        string nestedFields2 = nestedProperty.Substring(startField + 1, (lastField - 1) - startField);
                                                    }
                                                }
                                                else if (el2 is JArray)
                                                {
                                                    JArray jar = el2 as JArray;

                                                    if (_nestedFieldList.Contains(el2.Path.ToLower().Substring(el2.Path.ToLower().LastIndexOf('.') + 1)))
                                                        break;

                                                    foreach (JToken el3 in (jar as JContainer).Children())
                                                    {
                                                        if (el3 is JObject)
                                                        {
                                                            string _nestedField = _nestedFieldList.FirstOrDefault(f => f.ToLower().StartsWith(_poNested.Path.ToLower().Substring(_poNested.Path.IndexOf('.') + 1) + "("));

                                                            if (string.IsNullOrEmpty(_nestedField))
                                                            {
                                                                removeListNested.Add(_poNested);
                                                                break;
                                                            }

                                                            int startField2 = _nestedField.IndexOf("(");
                                                            int lastField2 = _nestedField.LastIndexOf(")");
                                                            string nestedFields2 = _nestedField.Substring(startField2 + 1, (lastField2 - 1) - startField2);

                                                            string[] _nestedFields = nestedFields2.Split(',');

                                                            JObject job = el3 as JObject;
                                                            foreach (JToken el4 in (job as JContainer).Children())
                                                            {
                                                                if (!_nestedFields.Contains(el4.Path.ToLower().Substring(el4.Path.ToLower().LastIndexOf('.') + 1)))
                                                                    removeListNested.Add(el4);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            foreach (JToken elNest in removeListNested)
                            {
                                elNest.Remove();
                            }

                            if (fields.Contains(nestedProperty))
                                fields.Remove(nestedProperty);

                            if (fields.Contains(el.Path.ToLower()))
                                fields.Remove(el.Path.ToLower());
                        }
                        else if (jprop != null && !fields.Contains(jprop.Name.ToLower()))
                        {
                            removeList.Add(el);
                        }
                    }
                    else
                    {
                        jobj = el as JObject;

                        foreach (JToken _joNested in (jobj as JContainer).Children().OrderBy(order => order.Parent))
                        {
                            if (fields.Any(field => field.ToLower().StartsWith(_joNested.Path.ToLower().Substring(_joNested.Path.IndexOf('.') + 1) + "(")))
                            {
                                string nestedProperty = fields.First(f => f.StartsWith(_joNested.Path.ToLower().Substring(_joNested.Path.IndexOf('.') + 1) + "("));
                                int startField = nestedProperty.IndexOf("(");
                                int lastField = nestedProperty.LastIndexOf(")");
                                string nestedFields = nestedProperty.Substring(startField + 1, (lastField - 1) - startField);

                                List<string> _nestedFieldList = GetNestedFiels(nestedFields); // nestedFields.Split(';').ToList();
                                JToken nestedProperties = _joNested.First();
                                List<JToken> removeListNested = new List<JToken>();
                                foreach (JToken elNested in (nestedProperties as JContainer).Children())
                                {
                                    if (elNested is JProperty)
                                    {
                                        pNested = elNested as JProperty;
                                        if (!_nestedFieldList.Contains(pNested.Path.ToLower().Substring(pNested.Path.IndexOf('.') + 1)))
                                            removeListNested.Add(pNested);
                                    }
                                    else if (elNested is JObject)
                                    {
                                        poNested = elNested as JObject;

                                        foreach (JToken _poNested in (poNested as JContainer).Children().OrderBy(order => order.Parent))
                                        {
                                            if (_nestedFieldList.Any(f => f.ToLower().StartsWith(_poNested.Path.ToLower().Substring(_poNested.Path.LastIndexOf('.') + 1) + "(")))
                                            {
                                                string field = _nestedFieldList.First(f => f.ToLower().StartsWith(_poNested.Path.ToLower().Substring(_poNested.Path.LastIndexOf('.') + 1) + "("));
                                                int startNestedField = field.IndexOf("(");
                                                int lastNestedField = field.LastIndexOf(")");
                                                string nestedFields2 = field.Substring(startNestedField + 1, (lastNestedField - 1) - startNestedField);

                                                List<string> _nestedFieldList2 = nestedFields2.Split(',').ToList(); // GetNestedFiels(nestedFields2); // nestedFields.Split(';').ToList();
                                                JToken nestedProperties2 = _poNested.First();
                                                List<JToken> removeListNested2 = new List<JToken>();
                                                foreach (JToken elNested2 in (nestedProperties2 as JContainer).Children())
                                                {
                                                    if (elNested2 is JProperty)
                                                    {
                                                        pNested = elNested2 as JProperty;
                                                        if (!_nestedFieldList2.Contains(pNested.Path.ToLower().Substring(pNested.Path.IndexOf('.') + 1)))
                                                            removeListNested.Add(pNested);
                                                    }
                                                    else if (elNested2 is JObject)
                                                    {
                                                        poNested = elNested2 as JObject;

                                                        foreach (JToken _poNested2 in (poNested as JContainer).Children().OrderBy(order => order.Parent))
                                                        {
                                                            if ((!_nestedFieldList2.Contains(_poNested2.Path.ToLower().Substring(_poNested2.Path.LastIndexOf('.') + 1))))
                                                                removeListNested.Add(_poNested2);
                                                        }
                                                    }
                                                }

                                            }

                                            else if ((!_nestedFieldList.Contains(_poNested.Path.ToLower().Substring(_poNested.Path.LastIndexOf('.') + 1))))
                                                removeListNested.Add(_poNested);
                                        }
                                    }
                                }
                                foreach (JToken elNest in removeListNested)
                                {
                                    elNest.Remove();
                                }

                                if (fields.Contains(nestedProperty) && el.Next == null)
                                    fields.Remove(nestedProperty);

                                if (fields.Contains(el.Path.ToLower()))
                                    fields.Remove(el.Path.ToLower());
                            }
                            else if (!fields.Contains(_joNested.Path.ToLower().Substring(_joNested.Path.IndexOf('.') + 1)))
                                removeList.Add(_joNested);
                        }
                    }
                }

                foreach (JToken el in removeList)
                {
                    el.Remove();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static List<string> GetNestedFiels(string fields)
        {
            if (!fields.Contains('(') && !fields.Contains(')'))
                return fields.Split(';').ToList();

            List<string> _fieldList = new List<string>();
            string _tempField = string.Empty;
            char[] _fieldArray = fields.ToCharArray();
            bool _skip = false;

            try
            {
                for (int i = 0; i < _fieldArray.Length; i++)
                {
                    if (_fieldArray[i] != ';' && _fieldArray[i] != ')')
                    {
                        _tempField += _fieldArray[i];

                        if (_fieldArray[i] == '(')
                        {
                            _skip = true;
                        }
                    }
                    else if (_fieldArray[i] == ';')
                    {
                        if (!_skip)
                        {
                            _fieldList.Add(_tempField);
                            _tempField = string.Empty;
                        }
                        else
                        {
                            _tempField += ',';
                        }
                    }
                    else if (_fieldArray[i] == ')')
                    {
                        _tempField += ')';
                        _fieldList.Add(_tempField);
                        _tempField = string.Empty;
                        _skip = false;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _fieldList;
        }
    }
}
