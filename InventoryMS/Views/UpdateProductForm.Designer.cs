﻿namespace InventoryMS.Views
{
    partial class UpdateProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateProductForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUpdateName = new System.Windows.Forms.TextBox();
            this.comboUpdateCategory = new System.Windows.Forms.ComboBox();
            this.txtUpdateQuantity = new System.Windows.Forms.TextBox();
            this.txtUpdateDescription = new System.Windows.Forms.TextBox();
            this.txtUpdateUnitPrice = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inventory Management System | Update Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter new name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stock:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Price:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Category:";
            // 
            // txtUpdateName
            // 
            this.txtUpdateName.Location = new System.Drawing.Point(280, 84);
            this.txtUpdateName.Name = "txtUpdateName";
            this.txtUpdateName.Size = new System.Drawing.Size(121, 22);
            this.txtUpdateName.TabIndex = 6;
            // 
            // comboUpdateCategory
            // 
            this.comboUpdateCategory.FormattingEnabled = true;
            this.comboUpdateCategory.Location = new System.Drawing.Point(280, 196);
            this.comboUpdateCategory.Name = "comboUpdateCategory";
            this.comboUpdateCategory.Size = new System.Drawing.Size(121, 24);
            this.comboUpdateCategory.TabIndex = 7;
            this.comboUpdateCategory.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtUpdateQuantity
            // 
            this.txtUpdateQuantity.Location = new System.Drawing.Point(280, 113);
            this.txtUpdateQuantity.Name = "txtUpdateQuantity";
            this.txtUpdateQuantity.Size = new System.Drawing.Size(121, 22);
            this.txtUpdateQuantity.TabIndex = 8;
            // 
            // txtUpdateDescription
            // 
            this.txtUpdateDescription.Location = new System.Drawing.Point(280, 140);
            this.txtUpdateDescription.Name = "txtUpdateDescription";
            this.txtUpdateDescription.Size = new System.Drawing.Size(121, 22);
            this.txtUpdateDescription.TabIndex = 9;
            // 
            // txtUpdateUnitPrice
            // 
            this.txtUpdateUnitPrice.Location = new System.Drawing.Point(280, 169);
            this.txtUpdateUnitPrice.Name = "txtUpdateUnitPrice";
            this.txtUpdateUnitPrice.Size = new System.Drawing.Size(121, 22);
            this.txtUpdateUnitPrice.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UpdateProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 297);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtUpdateUnitPrice);
            this.Controls.Add(this.txtUpdateDescription);
            this.Controls.Add(this.txtUpdateQuantity);
            this.Controls.Add(this.comboUpdateCategory);
            this.Controls.Add(this.txtUpdateName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateProductForm";
            this.Text = "UpdateProductForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUpdateName;
        private System.Windows.Forms.ComboBox comboUpdateCategory;
        private System.Windows.Forms.TextBox txtUpdateQuantity;
        private System.Windows.Forms.TextBox txtUpdateDescription;
        private System.Windows.Forms.TextBox txtUpdateUnitPrice;
        private System.Windows.Forms.Button button1;
    }
}