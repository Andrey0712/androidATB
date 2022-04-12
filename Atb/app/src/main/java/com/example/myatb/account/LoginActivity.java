package com.example.myatb.account;

import androidx.appcompat.app.AppCompatActivity;

import com.example.myatb.MainActivity;
import com.example.myatb.R;
import com.example.myatb.network.account.AccountService;
import com.example.myatb.network.account.dto.LoginDto;
import com.example.myatb.network.account.dto.LoginResponseDto;
import com.example.myatb.network.account.dto.ValidationRegisterDTO;
import com.google.android.material.textfield.TextInputEditText;
import com.google.android.material.textfield.TextInputLayout;
import com.google.gson.Gson;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {
    private TextView tvInfoLogin;
    private TextInputLayout textInputLoginEmail;
    private TextInputEditText txtLoginEmail;
    private TextInputLayout textInputLoginPassword;
    private TextInputEditText txtLoginPassword;




    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        tvInfoLogin=findViewById(R.id.tvInfoLogin);
        textInputLoginEmail=findViewById(R.id.textInputLoginEmail);
        txtLoginEmail=findViewById(R.id.txtLoginEmail);
        textInputLoginPassword=findViewById(R.id.textInputLoginPassword);
        txtLoginPassword=findViewById(R.id.txtLoginPassword);

    }

    public void  onLoginHandler(View view){
       //String text=editText.getText().toString();
        //tvInfoLogin.setText(text);
        LoginDto loginDto=new LoginDto();
        loginDto.setEmail(txtLoginEmail.getText().toString());
        loginDto.setPassword(txtLoginPassword.getText().toString());

        if(!validationFields(loginDto))
            return;

        AccountService.getInstance()
                .jsonApi()
                .login(loginDto)
                .enqueue(new Callback<LoginResponseDto>() {

                    @Override
                    public void onResponse(Call<LoginResponseDto> call, Response<LoginResponseDto> response) {
                        if(response.isSuccessful()){
                            LoginResponseDto data= response.body();
                            //tvInfo.setText("response is good");
                            Intent intent =new Intent(LoginActivity.this, MainActivity.class);
                            startActivity(intent);
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
                    public void onFailure(Call<LoginResponseDto> call, Throwable t) {
                        String str=t.toString();
                    }
                });
    }

    private  boolean validationFields(LoginDto loginDto){
        textInputLoginEmail.setError("");
        if(loginDto.getEmail().equals("")){
            textInputLoginEmail.setError("Вкажіть  E-mail");
            return false;
        }

        textInputLoginPassword.setError("");
        if (loginDto.getPassword().equals("")) {
            textInputLoginPassword.setError("Вкажіть пароль");
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
        textInputLoginEmail.setError(str);

        str = "";
        if (result.getErrors().getPassword() != null) {
            for (String item : result.getErrors().getPassword()) {
                str += item + "\n";
            }
        }
        textInputLoginPassword.setError(str);

    }


}