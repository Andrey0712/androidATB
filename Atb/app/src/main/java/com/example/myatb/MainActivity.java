package com.example.myatb;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.example.myatb.network.account.AccountService;
import com.example.myatb.network.account.dto.AccountResponseDTO;
import com.example.myatb.network.account.dto.RegisterDto;
import com.example.myatb.network.account.dto.RegisterErrorDTO;
import com.example.myatb.network.account.dto.ValidationRegisterDTO;
import com.google.android.material.textfield.TextInputEditText;
import com.google.android.material.textfield.TextInputLayout;
import com.google.gson.Gson;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    private TextView tvInfo;
    private TextInputLayout textFieldEmail;
    private TextInputEditText txtEmail;
    private TextInputLayout textFieldFirstName;
    private TextInputEditText txtFirstName;
    private TextInputLayout textFieldSecondName;
    private TextInputEditText txtSecondName;
    private TextInputLayout textFieldPhone;
    private TextInputEditText txtPhone;
    private TextInputLayout textPassword;
    private TextInputEditText txtPassword;
    private TextInputLayout textConfirmPassword;
    private TextInputEditText txtConfirmPassword;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        tvInfo=findViewById(R.id.tvInfo);
        textFieldEmail=findViewById(R.id.textFieldEmail);
        txtEmail=findViewById(R.id.txtEmail);
        textFieldFirstName=findViewById(R.id.textFieldFirstName);
        txtFirstName=findViewById(R.id.txtFirstName);
        textFieldSecondName=findViewById(R.id.textFieldSecondName);
        txtSecondName=findViewById(R.id.txtSecondName);
        textFieldPhone=findViewById(R.id.textFieldPhone);
        txtPhone=findViewById(R.id.txtPhone);
        textPassword=findViewById(R.id.textPassword);
        txtPassword=findViewById(R.id.txtPassword);
        textConfirmPassword=findViewById(R.id.textConfirmPassword);
        txtConfirmPassword=findViewById(R.id.txtConfirmPassword);

    }

    public void  handleClick(View view){
//        String text=editText.getText().toString();
//        tvInfo.setText(text);
        RegisterDto registerDto=new RegisterDto();
        registerDto.setEmail(txtEmail.getText().toString());
        registerDto.setFirstName(txtFirstName.getText().toString());
        registerDto.setSecondName(txtSecondName.getText().toString());
        registerDto.setPhone(txtPhone.getText().toString());
        registerDto.setPassword(txtPassword.getText().toString());
        registerDto.setConfirmPassword(txtConfirmPassword.getText().toString());

        if(!validationFields(registerDto))
            return;

        AccountService.getInstance()
                .jsonApi()
                .register(registerDto)
                .enqueue(new Callback<AccountResponseDTO>() {
                    @Override
                    public void onResponse(Call<AccountResponseDTO> call, Response<AccountResponseDTO> response) {
                        if(response.isSuccessful()){
                            AccountResponseDTO data= response.body();
                            tvInfo.setText("response is good");
                        }
                        else {
                            try {
                                showErrorsServer(response.errorBody().string());
                            }
                            catch (Exception e){
                                System.out.println("Error response parse body");
                            }
                        }


                    }


                    @Override
                    public void onFailure(Call<AccountResponseDTO> call, Throwable t) {
                      String str=t.toString();
                       int a=12;
                    }
                });
    }

    private  boolean validationFields(RegisterDto registerDto){
        textFieldEmail.setError("");
        if(registerDto.getEmail().equals("")){
            textFieldEmail.setError("Вкажіть  E-mail");
            return false;
        }
        textFieldFirstName.setError("");
        if (registerDto.getFirstName().equals("")) {
            textFieldFirstName.setError("Вкажіть ім'я");
            return false;
        }
        textFieldSecondName.setError("");
        if (registerDto.getSecondName().equals("")) {
            textFieldFirstName.setError("Вкажіть Призвище");
            return false;
        }
        textFieldPhone.setError("");
        if (registerDto.getPhone().equals("")) {
            textFieldFirstName.setError("Вкажіть телефон");
            return false;
        }
        textPassword.setError("");
        if (registerDto.getPassword().equals("")) {
            textFieldFirstName.setError("Вкажіть пароль");
            return false;
        }
        textConfirmPassword.setError("");
        if (registerDto.getConfirmPassword().equals("")) {
            textFieldFirstName.setError("Підтвердіть пароль");
            return false;
        }
        return  true;
    }

    private void showErrorsServer(String json) {
        Gson gson = new Gson();
        ValidationRegisterDTO result = gson.fromJson(json, ValidationRegisterDTO.class);
        String str = "";
        if (result.getErrors().getEmail() != null) {
            for (String item : result.getErrors().getEmail()) {
                str += item + "\n";
            }
        }
        textFieldEmail.setError(str);

        str = "";
        if (result.getErrors().getFirstName() != null) {
            for (String item : result.getErrors().getFirstName()) {
                str += item + "\n";
            }
        }
        textFieldFirstName.setError(str);

        str = "";
        if (result.getErrors().getSecondName() != null) {
            for (String item : result.getErrors().getSecondName()) {
                str += item + "\n";
            }
        }
        textFieldSecondName.setError(str);

        str = "";
        if (result.getErrors().getPhone() != null) {
            for (String item : result.getErrors().getPhone()) {
                str += item + "\n";
            }
        }
        textFieldPhone.setError(str);

        str = "";
        if (result.getErrors().getPassword() != null) {
            for (String item : result.getErrors().getPassword()) {
                str += item + "\n";
            }
        }
        textPassword.setError(str);

        str = "";
        if (result.getErrors().getConfirmPassword() != null) {
            for (String item : result.getErrors().getConfirmPassword()) {
                str += item + "\n";
            }
        }
        textConfirmPassword.setError(str);


    }
}