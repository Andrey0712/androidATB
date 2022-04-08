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


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        tvInfo=findViewById(R.id.tvInfo);
        textFieldEmail=findViewById(R.id.textFieldEmail);
        txtEmail=findViewById(R.id.txtEmail);
    }

    public void  handleClick(View view){
//        String text=editText.getText().toString();
//        tvInfo.setText(text);
        RegisterDto registerDto=new RegisterDto();
        registerDto.setEmail(txtEmail.getText().toString());

        if(!validationFilds(registerDto))
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

    private  boolean validationFilds(RegisterDto registerDto){
        textFieldEmail.setError("");
        if(registerDto.getEmail().equals("")){
            textFieldEmail.setError("Вкажіть  E-mail");
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
            textFieldEmail.setError(str);
        } else
            textFieldEmail.setError("");

    }
}