using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
   private static TextWriter instance;
   
   private List<TextWriterSingle> textWriterSingleList;
   
   private void Awake()
   {
      instance = this;
      textWriterSingleList = new List<TextWriterSingle>();
   }

   public TextMeshProUGUI FindWriter(TextMeshProUGUI uiText)
   {
      for (int i = 0; i < textWriterSingleList.Count; i++)
      {
         if (textWriterSingleList[i].GetUiText() == uiText)
         {
            return textWriterSingleList[i].GetUiText();
         }
      }
      return null;
   }

   public static void AddWriter_Static(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
   {
      instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters);
   }
   
   private void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
   {
      textWriterSingleList.Add (new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters));
   }

   public static void RemoveWriter_Static(TextMeshProUGUI uiText)
   {
      instance.RemoveWriter(uiText);
   }
   
   private void RemoveWriter(TextMeshProUGUI uiText)
   {
      for (int i = 0; i < textWriterSingleList.Count; i++)
      {
         if (textWriterSingleList[i].GetUiText() == uiText)
         {
            textWriterSingleList.RemoveAt(i);
            i--;
         }
      }
   }

   public static bool CheckIfActiveWriter_Static()
   {
      return instance.CheckIfActiveWriter();
   }
   
   private bool CheckIfActiveWriter()
   {
      return textWriterSingleList.Count > 0;
   }

   private void Update()
   {
      for (int i = 0; i < textWriterSingleList.Count; i++)
      {
         textWriterSingleList[i].Update();
      }
   }

   //Single TextWriter Instance
   public class TextWriterSingle
   {
      private TextMeshProUGUI uiText;
      private string textToWrite;
      private int characterIndex;
      private float timePerCharacter;
      private float timer;
      private bool invisibleCharacters;
      
      public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
      {
         this.uiText = uiText;
         this.textToWrite = textToWrite;
         this.timePerCharacter = timePerCharacter;
         this.invisibleCharacters = invisibleCharacters;
         characterIndex = 0;
      }
      
      public void Update()
      {
         if (uiText != null && uiText.text != textToWrite)
         {
            timer -= Time.deltaTime;
         
            while (timer < 0f)
            {
               //Display next character
               timer += timePerCharacter;
               characterIndex++;
               string text = textToWrite.Substring(0, characterIndex);
            
               if (invisibleCharacters)
               {
                  text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
               }
            
               uiText.text = text;
               
               if (characterIndex >= textToWrite.Length)
               {
                  uiText = null;
                  return;
               }
            }
         }
      }

      public TextMeshProUGUI GetUiText()
      {
         return uiText;
      }
   }
}
