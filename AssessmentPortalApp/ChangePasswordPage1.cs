﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AssessmentPortalApp
{
    public partial class ChangePasswordPage1 : Form
    {
        private string connectionString = "data source=akshu\\sqlexpress; initial catalog=AssessmentPortalDB; user id=sa;password=akshu";
        public int userId;
        public string userName;
        public int roleId;

        public ChangePasswordPage1()
        {
            InitializeComponent();
        }

        private void ChangePasswordPage1_Load(object sender, EventArgs e)
        {
            lblMsg.Text = userName;
        }

        public bool ValidateFields()
        {
            bool isValid = true;

            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtExistingPass.Text))
            {
                errorProvider1.SetError(txtExistingPass, "Please enter your existing mobile number");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPass.Text))
            {
                errorProvider1.SetError(txtNewPass, "Please enter your new mobile number");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPass.Text))
            {
                errorProvider1.SetError(txtConfirmPass, "Please confirm your mobile number");
                isValid = false;
            }

            if (txtNewPass.Text != txtConfirmPass.Text)
            {
                errorProvider1.SetError(txtConfirmPass, "Mobile numbers do not match");
                isValid = false;
            }

            return isValid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            if (txtExistingPass.Text.Trim() == txtNewPass.Text.Trim())
            {
                errorProvider1.SetError(txtNewPass, "New mobile number cannot be the same as the existing mobile number");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Query to check if the existing mobile number matches the user ID
                string checkQuery = "SELECT COUNT(*) FROM AllUsers WHERE userid = @userid AND mblno = @existingMblno";
                using (SqlCommand cmd = new SqlCommand(checkQuery, con))
                {
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@existingMblno", txtExistingPass.Text.Trim());

                    try
                    {
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count == 0)
                        {
                            errorProvider1.SetError(txtExistingPass, "Existing mobile number is incorrect");
                            return;
                        }

                        // Update query to change the mobile number for the user ID
                        string updateQuery = "UPDATE AllUsers SET mblno = @newMblno WHERE userid = @userid";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                        {
                            updateCmd.Parameters.AddWithValue("@newMblno", txtNewPass.Text.Trim());
                            updateCmd.Parameters.AddWithValue("@userid", userId);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Mobile number updated successfully.");
                                ClearFields();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update mobile number.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FirstPage wlcmPage = new FirstPage();
            wlcmPage.Show();
            this.Hide();
        }

        private void ClearFields()
        {
            txtExistingPass.Clear();
            txtNewPass.Clear();
            txtConfirmPass.Clear();
            lblMsgBelow.Text = string.Empty;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AllUsersLoginPage1 page = new AllUsersLoginPage1();
            page.Show();
            this.Hide();
        }
    }
}
