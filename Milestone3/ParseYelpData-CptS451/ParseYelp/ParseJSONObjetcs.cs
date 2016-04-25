using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using System.Text.RegularExpressions;

namespace parse_yelp
{
    public static class globalVar
    {
        public static string bid { get; set; }
        public static bool subattribute { get; set; }
        public static string attribute { get; set; }
    }
    class ParseJSONObjects
    {              
        Categories category;
        List<string> attributeList = new List<string>();

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
        public StringBuilder aProcess(JsonObject o, StringBuilder sb)
        {
            int a;
            string[] k = o.Keys.ToArray<string>();
            JsonValue[] v = o.Values.ToArray();
            string temp;
            for (int i = 0; i < k.Length; i++)
            {
                temp = cleanTextforSQL(v[i].ToString());
                if (temp.StartsWith("{"))
                {
                    //sb.Append("insert into attributes (bid, name, _value) values (" + globalVar.bid + ", " + k[i] + ", NULL);\n");
                    
                    globalVar.attribute = "\"" + k[i] + "\"";
                    globalVar.subattribute = true;
                    sb = aProcess((JsonObject)v[i], sb);
                    globalVar.subattribute = false;
                }
                else
                {
                    //if (temp == "true")
                    //{ sb.Append("1, "); }
                    //else if (temp == "false")
                    //{ sb.Append("0, "); }
                    //else if (int.TryParse(temp, out a))
                    //{
                    //    sb.Append(temp + ", ");
                    //}
                    sb.Append("insert into attributes (bid, name, _value) values (" + globalVar.bid + ", ");
                    if (globalVar.subattribute == true)
                    {
                        sb.Append(globalVar.attribute + " ");
                    }
                    sb.Append("\"" + k[i] + "\"" + ", ");
                    if (temp[0] == '"')
                    {
                        sb.Append(temp + ")");
                    }
                    else
                    { sb.Append("\"" + temp.ToString() + "\");\r\n"); }

                }
            }
            return sb;
        }
        public StringBuilder rProcess(JsonObject o, StringBuilder sb)
        {
            int a;
            string[] k = o.Keys.ToArray<string>();
            JsonValue[] v = o.Values.ToArray();
            string temp;
            for (int i = 0; i < k.Length; i++)
            {
                temp = cleanTextforSQL(v[i].ToString());
                if (temp.StartsWith("{"))
                {
                    sb = rProcess((JsonObject)v[i], sb);
                }
                else
                {
                    if (temp == "true")
                    { sb.Append("1, "); }
                    else if (temp == "false")
                    { sb.Append("0, "); }
                    else if (int.TryParse(temp, out a))
                    {
                        sb.Append(temp + ", ");
                    }
                    else
                    { sb.Append("\"" + temp.ToString() + "\", "); }
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
            string bid = v[0].ToString();
            globalVar.bid = bid;
            string zip = "";
            string address = cleanTextforSQL( v[1].ToString());
            //CREATE ZIP CODE
            for (int x = address.Length - 5; x < address.Length; x++)
            {
                zip += address[x];
            }
            char[] trim = { ' ' };
            zip = zip.TrimStart(trim);
            int open = 0;
            if (v[3].ToString() == "true")
            {
                open = 1;
            }
            //CREATE INSERT STATEMENTS FOR BUSINESS (bid, name, city, state_code, zip)
            sb.Append("insert into businesses (bid, name, avg_rev, num_revs, city, state_code, zipcode, open) values (" + v[0].ToString() + ", " + v[7].ToString() + ", ");
            sb.Append(v[11].ToString() + ", "+ v[6].ToString() + ", " + v[5].ToString() + ", " + v[10].ToString() + ", " + zip + ", " + open +");\r\n");
            //CREATE insert statements for categories (name, bid)
            if (v[4].ToArray().Count() > 0)
            {
                
                for (j = 0; j < v[4].ToArray().Length - 1; j++)
                {
                    sb.Append("insert into categories (name, bid) values (\"");
                    sb.Append(cleanTextforSQL(v[4][j].ToString()) + "\", " + bid + ");\r\n");
                }
            }
            //CREATE insert statements for attributes (bid, ....)
              
            sb = aProcess((JsonObject)v[13], sb);
            string s = sb.ToString();
            char[] charsToTrim = { ',', ' ' };
            s = s.TrimEnd(charsToTrim);
            s += "\r\n";
            //for (int i = 0; i < keyArray.Count(); i++)
            //{
            //    //Console.Write(sb.ToString());
            //    //if (i == )
            //    //{ 
            //        //recursion
            //        //sb.Append(keyArray[i] + ": " + "\r\n");
            //        //sb = rProcess((JsonObject)v[i], sb);
                 
            //    //}
            //    else if (i == 
            //    else if (i == 2 || i == 8)
            //    { 
            //        //do nothing
            //    }
            //    else if (i == 4)
            //    {
                    
            //        for (j = 0; j < v[4].ToArray().Length - 1; j++)
            //        {
            //            sb.Append("insert into categories (name, bid) values (");
            //            sb.Append(cleanTextforSQL(v[4][j].ToString()) + ", " + bid + ");");
            //        }
            //    //    //list
            //    //    sb.Append(keyArray[i] + ": " + "\r\n");
            //    //    for (j = 0; j < v[i].ToArray().Length - 1; j++)
            //    //    {
            //    //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + ", ");
            //    //    }
            //        if (v[i].ToArray().Count() > 0)
            //        { sb.Append(cleanTextforSQL(v[i][j].ToString()) + "\r\n"); }

            //    }
            //    //else
            //    //{
            //    //    sb.Append(keyArray[i] + ": " + cleanTextforSQL(v[i].ToString()) + "\r\n");
            //    //}
            //}
            //Console.WriteLine(s);

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
            return s;
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
            //Console.WriteLine("about to do stuff");
            //string funny = "0", useful = "0", cool = "0";
            
            sb.Append("insert into reviews (rid, uid, bid, stars, funny, useful, cool, date, text) values "
                + "(" + v[2].ToString() + ", " + v[1].ToString() + ", " + v[7].ToString() + ", " + v[3].ToString() + ", ");
            //    + v[0][0].ToString() + ", " + v[0][1].ToString() + ", " + v[0][2].ToString() + ", " + 
            sb = rProcess((JsonObject)v[0], sb);
            string output = sb.ToString();
            char[] trimchars = {' ', ','};
            output.TrimEnd(trimchars);
            output += v[4].ToString() + ", " + "\"" +  TruncateReviewText(cleanTextforSQL(v[5].ToString()) );
            output += "\");\r\n";
            //for (int i = 0; i < keyArray.Count(); i++)
            //{
            //    //Console.Write(sb.ToString());
            //    if (i == 0)
            //    {
            //        //recursion
            //        sb.Append(keyArray[i] + ": " + "\r\n");
            //        sb = rProcess((JsonObject)v[i], sb);
                    
            //    }
            //    //no lists
            //    //else if (i == 4 || i == 8)
            //    //{
            //    //    //list
            //    //    sb.Append(keyArray[i] + ": " + "\r\n");
            //    //    for (j = 0; j < v[i].ToArray().Length - 1; j++)
            //    //    {
            //    //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + ", ");
            //    //    }
            //    //    if (v[i].ToArray().Count() > 0)
            //    //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + "\r\n");
            //    //}
            //    else if (i == 5)
            //    {
            //        sb.Append(keyArray[i] + ": " + TruncateReviewText(cleanTextforSQL(v[i].ToString())) + "\r\n");
            //    }
            //    else
            //    {
            //        sb.Append(keyArray[i] + ": " + cleanTextforSQL(v[i].ToString()) + "\r\n");
            //    }
            //}
            ////Console.WriteLine(sb.ToString());


            //////Example: extract business_id and reviewtext
            ////String review_id = cleanTextforSQL(my_jsonStr["review_id"].ToString());
            //////You may limit the text lenght for review text 
            ////String reviewtext = TruncateReviewText(cleanTextforSQL(my_jsonStr["text"].ToString()));

            ///* EXTRACT OTHER KEY VALUES */
            return output;

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
            sb.Append("insert into users (uid, name) values (\"" + cleanTextforSQL(v[4].ToString()) +"\", \"" + cleanTextforSQL(v[3].ToString())+ "\");\r\n");
            //for (int i = 0; i < keyArray.Count(); i++)
            //{
            //    //Console.Write(sb.ToString());
            //    if (i == 1)
            //    {
            //        //recursion
            //        sb.Append(keyArray[i] + ": " + "\r\n");
            //        sb = rProcess((JsonObject)v[i], sb);
                    
            //    }
            //    else if (i == 5 || i == 9 || i == 10)
            //    { 
            //        //do nothing
            //    }
            //    //else if (i == 5)
            //    //{
            //    //    //list
            //    //    sb.Append(keyArray[i] + ": " + "\r\n");
            //    //    for (j = 0; j < v[i].ToArray().Length - 1; j++)
            //    //    {
            //    //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + ", ");
            //    //    }
            //    //    if (v[i].ToArray().Count() > 0)
            //    //        sb.Append(cleanTextforSQL(v[i][j].ToString()) + "\r\n");
            //    //}
            //    else
            //    {
            //        sb.Append(keyArray[i] + ": " + cleanTextforSQL(v[i].ToString()) + "\r\n");
            //    }
            //}
            ////Console.WriteLine(sb.ToString());
            ////Example: extract user_id
            ////String user_id = cleanTextforSQL(my_jsonStr[my_jsonStr.Keys.ToArray()[4]].ToString());
            

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
