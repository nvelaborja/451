using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using System.Text.RegularExpressions;

namespace parse_yelp
{
    
    class ParseJSONObjects
    {              
        Categories category;

        public ParseJSONObjects( )
        {
            category = new Categories();
        }
        
        public void Close( )
        {
        }

        private int maxLength = 5000;
        private string cleanTextforSQL(string inStr)
        {
            String outStr = Encoding.GetEncoding("iso-8859-1").GetString(Encoding.UTF8.GetBytes(inStr));
            outStr = outStr.Replace("\"", "").Replace("'", " ").Replace(@"\n", " ").Replace(@"\u000a", " ").Replace("\\", " ").Replace("é", "e").Replace("ê", "e").Replace("Ã¼", "A").Replace("Ã", "A").Replace("¤", "").Replace("©", "c").Replace("¶","");
            outStr = Regex.Replace(outStr,@"[^\u0020-\u007E]", "?");
            
            //Only get he first maxLength chars. Set maxLength to the max length of your attribute.
            return outStr.Substring(0, Math.Min(outStr.Length, maxLength));
        }

        
        private string TruncateReviewText(string longText)
        {
            int maxTextLength = 250;
            return longText.Substring(0, Math.Min(maxTextLength, longText.Length)) + "...";
        }

        
        /* Extract business information*/
        public StringBuilder rProcess(JsonObject o, StringBuilder sb)
        {
            string[] k = o.Keys.ToArray<string>();
            JsonValue[] v = o.Values.ToArray();
            string temp;
            for (int i = 0; i < k.Length; i++)
            {
                sb.Append(cleanTextforSQL(k[i].ToString()) + ": ");
                temp = cleanTextforSQL( v[i].ToString());
                if (temp.StartsWith("{"))
                {
                    sb = rProcess((JsonObject)v[i], sb);
                }
                else
                {
                    sb.Append(temp + "\r\n");
                }
            }
            return sb;
        }
        public string ProcessBusiness(JsonObject my_jsonStr)
        {            
            //You may extract values for certain keys by specifying the key name. 
            //Example: extract business_id
            StringBuilder sb = new StringBuilder();
            string[] keyArray = my_jsonStr.Keys.ToArray<string>();
            JsonValue[] v = my_jsonStr.Values.ToArray();
            //JsonObject o;
            //string temp;
            int j;
            //attributes and hours are nested
            //categories and neighborhood are lists
            for (int i = 0; i < keyArray.Count(); i++)
            {
                //Console.Write(sb.ToString());
                if (i == 13)
                { 
                    //recursion
                    sb.Append(keyArray[i] + ": " + "\r\n");
                    sb = rProcess((JsonObject)v[i], sb);
                 
                }
                else if (i == 2 || i == 8)
                { 
                    //do nothing
                }
                else if (i == 4)
                {
                    //list
                    sb.Append(keyArray[i] + ": " + "\r\n");
                    for (j = 0; j < v[i].ToArray().Length - 1; j++)
                    {
                        sb.Append(cleanTextforSQL(v[i][j].ToString()) + ", ");
                    }
                    if (v[i].ToArray().Count() > 0)
                        sb.Append(cleanTextforSQL(v[i][j].ToString()) + "\r\n");
                }
                else
                {
                    sb.Append(keyArray[i] + ": " + cleanTextforSQL(v[i].ToString()) + "\r\n");
                }
            }
            //Console.WriteLine(sb.ToString());

            //String business_id = my_jsonStr["business_id"].ToString();
            //sb.Append("business_id:  " + cleanTextforSQL(business_id) + "\r\n");
            ///*To retrieve list of Keys in JSON :
            //        my_jsonStr.Keys.ToArray()[0]  is the "business_id" key. */
            ///*To retrieve list of Values in JSON 
            //        my_jsonStr.Values.ToArray()[0]  is the value for "business_id".*/
            
            ////Alternative ways to extract business_id:
            //business_id = my_jsonStr[my_jsonStr.Keys.ToArray()[0]].ToString();
            //business_id = my_jsonStr.Values.ToArray()[0].ToString();

            ///* EXTRACT OTHER KEY VALUES */
            //String full_address = my_jsonStr["full_address"].ToString();
            //sb.Append("full_address: " + cleanTextforSQL(full_address) + "\r\n");
            ////DEAL WITH HOURS
            //String hours = my_jsonStr["hours"].ToString();
            //String open = my_jsonStr["open"].ToString();
            //sb.Append("open: " + cleanTextforSQL(open) + "\r\n");
             

            //Clean text and remove any characters that might cause errors in MySQL.
            return (sb.ToString());
        }


        /* Extract review information*/
        public string ProcessReviews(JsonObject my_jsonStr)
        {
            StringBuilder sb = new StringBuilder();
            string[] keyArray = my_jsonStr.Keys.ToArray<string>();
            JsonValue[] v = my_jsonStr.Values.ToArray();
            //JsonObject o;
            //string temp;
            //int j;
            //attributes and hours are nested
            //categories and neighborhood are lists
            for (int i = 0; i < keyArray.Count(); i++)
            {
                //Console.Write(sb.ToString());
                if (i == 0)
                {
                    //recursion
                    sb.Append(keyArray[i] + ": " + "\r\n");
                    sb = rProcess((JsonObject)v[i], sb);
                    
                }
                //no lists
                //else if (i == 4 || i == 8)
                //{
                //    //list
                //    sb.Append(keyArray[i] + ": " + "\r\n");
                //    for (j = 0; j < v[i].ToArray().Length - 1; j++)
                //    {
                //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + ", ");
                //    }
                //    if (v[i].ToArray().Count() > 0)
                //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + "\r\n");
                //}
                else if (i == 5)
                {
                    sb.Append(keyArray[i] + ": " + TruncateReviewText(cleanTextforSQL(v[i].ToString())) + "\r\n");
                }
                else
                {
                    sb.Append(keyArray[i] + ": " + cleanTextforSQL(v[i].ToString()) + "\r\n");
                }
            }
            //Console.WriteLine(sb.ToString());


            ////Example: extract business_id and reviewtext
            //String review_id = cleanTextforSQL(my_jsonStr["review_id"].ToString());
            ////You may limit the text lenght for review text 
            //String reviewtext = TruncateReviewText(cleanTextforSQL(my_jsonStr["text"].ToString()));

            /* EXTRACT OTHER KEY VALUES */
            return (sb.ToString());

        }

        /* Extract review information*/
        public string ProcessUsers(JsonObject my_jsonStr)
        {
            StringBuilder sb = new StringBuilder();
            string[] keyArray = my_jsonStr.Keys.ToArray<string>();
            JsonValue[] v = my_jsonStr.Values.ToArray();
            //JsonObject o;
            //string temp;
            //int j;
            //attributes and hours are nested
            //categories and neighborhood are lists
            for (int i = 0; i < keyArray.Count(); i++)
            {
                //Console.Write(sb.ToString());
                if (i == 1)
                {
                    //recursion
                    sb.Append(keyArray[i] + ": " + "\r\n");
                    sb = rProcess((JsonObject)v[i], sb);
                    
                }
                else if (i == 5 || i == 9 || i == 10)
                { 
                    //do nothing
                }
                //else if (i == 5)
                //{
                //    //list
                //    sb.Append(keyArray[i] + ": " + "\r\n");
                //    for (j = 0; j < v[i].ToArray().Length - 1; j++)
                //    {
                //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + ", ");
                //    }
                //    if (v[i].ToArray().Count() > 0)
                //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + "\r\n");
                //}
                else
                {
                    sb.Append(keyArray[i] + ": " + cleanTextforSQL(v[i].ToString()) + "\r\n");
                }
            }
            //Console.WriteLine(sb.ToString());
            //Example: extract user_id
            //String user_id = cleanTextforSQL(my_jsonStr[my_jsonStr.Keys.ToArray()[4]].ToString());
            

            ///* EXTRACT OTHER KEY VALUES */
            //return ("user_id:  " + user_id );
            return sb.ToString();
        }


        /* The INSERT statement for category tuples*/
        public string ProcessBusinessCategories(JsonObject my_jsonStr)
        {
            String insertString = "";
            JsonArray categories = (JsonArray)my_jsonStr["categories"];
            //append an INSERT statement to insertString for each category of the business 
            for (int i = 0; i < categories.Count; i++)
                insertString = insertString + "INSERT INTO businessCategory (business_id, category) VALUES ("
                                + "'" + my_jsonStr["business_id"].ToString().Replace("\"", "") + "' , "
                                + "'" + cleanTextforSQL(categories[i].ToString()) + "'"
                                + ");"
                                + "\n"; //append a new line
            return insertString;
        }
                               

    }
}
